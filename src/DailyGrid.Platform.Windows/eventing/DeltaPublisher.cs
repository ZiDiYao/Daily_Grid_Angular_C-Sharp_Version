namespace DailyGrid.Platform.Windows.eventing;

using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Channels;

public sealed class DeltaPublisher : IAsyncDisposable
{
    private readonly ChannelWriter<long> _writer;
    private readonly CancellationTokenSource _cts = new();
    private readonly Subject<long> _manual = new();          // 手动触发来源
    private readonly IDisposable _subscription;

    // 你可以把 period/scheduler 做成可配置，方便测试
    public DeltaPublisher(
        ChannelWriter<long> writer,
        TimeSpan? period = null,
        IScheduler? scheduler = null)
    {
        _writer = writer ?? throw new ArgumentNullException(nameof(writer));

        var tickPeriod = period ?? TimeSpan.FromSeconds(1);
        var runOn = scheduler ?? TaskPoolScheduler.Default;

        // 1) 定时来源：每秒一个 tick
        IObservable<long> timerSource =
            Observable.Interval(tickPeriod, runOn)
                      .Select(_ => CreateDelta()); // 这里你换成真实采集逻辑

        // 2) 合并：定时 + 手动
        IObservable<long> merged =
            timerSource.Merge(_manual.ObserveOn(runOn));

        // 3) 关键：顺序写入 Channel（Concat 保证一次写完再写下一个）
        _subscription =
            merged
                .Select(delta =>
                    Observable.FromAsync(ct => _writer.WriteAsync(delta, ct).AsTask()))
                .Concat()
                .Subscribe(
                    _ => { }, // 每次写入完成的回调（一般不用）
                    ex =>
                    {
                        // 出错时让 Channel 完成，消费者能感知
                        _writer.TryComplete(ex);
                    },
                    () =>
                    {
                        // 正常结束时完成 Channel
                        _writer.TryComplete();
                    });
    }

    // 你现在先把它写死成 1 也行，后面再接入你的 delta 计算
    private static long CreateDelta()
    {
        // TODO: 从你的键鼠采集器/聚合器拿到 delta
        return 1;
    }

    /// <summary>
    /// 手动触发一次 publish（比如采集器推送、或某些事件触发）
    /// </summary>
    public void TryPublish(long delta)
    {
        // Subject 在 Dispose 后会抛异常；这里你可以选择 try/catch 或者加状态位
        _manual.OnNext(delta);
    }

    public ValueTask DisposeAsync()
    {
        // 1) 停止后续发送
        _cts.Cancel();

        // 2) 终止手动来源
        _manual.OnCompleted();
        _manual.Dispose();

        // 3) 释放订阅（停止 Rx 管道）
        _subscription.Dispose();

        // 4) 完成 Channel（如果前面没完成）
        _writer.TryComplete();

        _cts.Dispose();
        return ValueTask.CompletedTask;
    }
}
