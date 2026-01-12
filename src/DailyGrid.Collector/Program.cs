using System;
using System.Threading;
using DailyGrid.Platform.Windows.Hooks;

int keyDownCount = 0;

using var hook = new LowLevelKeyboardHook();
hook.OnKeyDown = _ => keyDownCount++;
hook.Start();

Thread.Sleep(5000);

hook.Stop();
Console.WriteLine("KeyDownCount:" + keyDownCount);
