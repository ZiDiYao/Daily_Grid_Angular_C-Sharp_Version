namespace DailyGrid.Platform.Windows.Hooks;

public sealed class HookManager : IDisposable
{
    private readonly List<IHook> _hooks = new();
    private readonly List<Thread> _threads = new();
    private readonly CancellationTokenSource _cts = new();

    public void Register(IHook hook) => _hooks.Add(hook);

    public void DeployHooks()
    {
        if (_threads.Count > 0) return;

        foreach (var hook in _hooks)
        {
            var t = new Thread(() => RunSingle(hook))
            {
                IsBackground = false,
                Name = $"HookThread-{hook.Name}"
            };
            _threads.Add(t);
            t.Start();
        }
    }

    private void RunSingle(IHook hook)
    {
        hook.Start();
        try
        {
            while (!_cts.IsCancellationRequested)
                Thread.Sleep(50);
        }
        finally
        {
            hook.Stop();
        }
    }

    public void StopAll()
    {
        _cts.Cancel();
        foreach (var t in _threads) t.Join();
        _threads.Clear();
    }

    public void Dispose()
    {
        StopAll();
        foreach (var h in _hooks) h.Dispose();
        _cts.Dispose();
    }
}
