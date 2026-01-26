using System;
using System.Reactive.Linq;
using System.Threading;

using DailyGrid.Platform.Windows.Hooks;
using DailyGrid.Core.Contracts.Events;

var mgr = new HookManager();
mgr.Register(new LowLevelKeyboardHook());

var sub = mgr.Events
    .OfType<KeyDownEvent>()
    .Buffer(TimeSpan.FromSeconds(1))
    .Subscribe(buf =>
    {
        Console.WriteLine($"KeyDown/s = {buf.Count}");
    });

mgr.DeployHooks();
Console.WriteLine("Running... press keys. Press Ctrl+C to stop.");

using var done = new ManualResetEventSlim(false);
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;  
    done.Set();
};

done.Wait();

sub.Dispose();
mgr.StopAll();
mgr.Dispose();
