#if DEBUG
namespace DailyGrid.Core.eventing;

using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

public static class EventingSmokeTest
{
    public static async Task RunAsync()
    {
        var channel = Channel.CreateBounded<long>(new BoundedChannelOptions(128)
        {
            SingleWriter = true,
            SingleReader = true,
            FullMode = BoundedChannelFullMode.Wait
        });

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));

        // 模拟 core 里某个 producer：每 200ms 写一个值
        var writer = Task.Run(async () =>
        {
            long n = 0;
            while (!cts.IsCancellationRequested)
            {
                await channel.Writer.WriteAsync(++n, cts.Token);
                await Task.Delay(200, cts.Token);
            }
        }, cts.Token);

        int count = 0;

        try
        {
            await foreach (var v in channel.Reader.ReadAllAsync(cts.Token))
            {
                Console.WriteLine($"[Core Smoke] {v}");
                if (++count >= 5) break;
            }
        }
        catch (OperationCanceledException) { }

        channel.Writer.TryComplete();
        try { await writer; } catch { }

        if (count == 0)
            throw new InvalidOperationException("Core smoke test failed: received 0 items.");
    }
}
#endif
