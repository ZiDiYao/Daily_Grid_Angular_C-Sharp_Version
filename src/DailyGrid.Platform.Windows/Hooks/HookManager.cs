namespace DailyGrid.Platform.Windows.Hooks;

using System;
using System.Collections.Generic;
using System.Threading;
using DailyGrid.Core.Contracts.Events;
using DailyGrid.Platform.Windows.Interop.Win32;

public sealed class HookManager : IDisposable
{
    // 容器，用于存放所有注册的 Hook
    private readonly List<IHook> _hooks = new();

    // 线程容器
    private readonly List<Thread> _threads = new();

    // 线程 ID 容器
    private readonly List<uint> _threadIds = new();

    private readonly CancellationTokenSource _cts = new();
    private readonly HookEventPublisher _publisher = new();

    public IObservable<ITelemetryEvent> Events => _publisher.Events;

    public void Register(IHook hook)
    {
        hook.AttachPublisher(_publisher);
        hook.AttachPublisher(_publisher);
        _hooks.Add(hook);
    }

    public void DeployHooks()
    {
        if (_threads.Count > 0) return;

        foreach (var hook in _hooks)
        {
            var t = new Thread(() => RunSingleWithMessagePump(hook))
            {
                IsBackground = false,
                Name = $"HookThread-{hook.Name}"
            };
            _threads.Add(t);
            t.Start();
        }
    }

    private void RunSingleWithMessagePump(IHook hook)
    {
        // 记录当前线程ID，供 StopAll PostThreadMessage 使用
        uint tid = Kernel32.GetCurrentThreadId();
        lock (_threadIds) _threadIds.Add(tid);

        // 关键：确保该线程有消息队列
        User32Hooks.MSG dummy;
        User32Hooks.PeekMessageW(out dummy, IntPtr.Zero, 0, 0, 0);

        hook.Start();

        try
        {
            // 消息泵：WM_QUIT 会让 GetMessageW 返回 0
            User32Hooks.MSG msg;
            while (!_cts.IsCancellationRequested &&
                   User32Hooks.GetMessageW(out msg, IntPtr.Zero, 0, 0) > 0)
            {
                User32Hooks.TranslateMessage(ref msg);
                User32Hooks.DispatchMessageW(ref msg);
            }
        }
        finally
        {
            try { hook.Stop(); } catch { }
        }
    }

    public void StopAll()
    {
        _cts.Cancel();

        // 给所有 hook 线程发 WM_QUIT 让 GetMessageW 退出
        lock (_threadIds)
        {
            foreach (var tid in _threadIds)
                User32Hooks.PostThreadMessageW(tid, User32Hooks.WM_QUIT, UIntPtr.Zero, IntPtr.Zero);
        }

        foreach (var t in _threads) t.Join();
        _threads.Clear();
        lock (_threadIds) _threadIds.Clear();
    }

    public void Dispose()
    {
        StopAll();
        foreach (var h in _hooks) h.Dispose();
        _publisher.Dispose();
        _cts.Dispose();
    }
}
