// namespace DailyGrid.Host;

// using System;
// using System.Runtime.InteropServices;
// using System.Threading;
// using System.Threading.Tasks;
// using DailyGrid.Platform.Windows.Hooks;
// using DailyGrid.Platform.Windows.Interop.Win32;

// public static class HookSmoke
// {
//     public static Task RunAsync()
//     {
//         using var hook = new LowLevelKeyboardHook();

//         long count = 0;
//         hook.OnKeyDown = vk =>
//         {
//             count++;
//             Console.WriteLine($"KeyDown vk={vk}, count={count}");
//         };

//         Console.WriteLine("Starting keyboard hook. Press some keys for 5 seconds...");
//         hook.Start();

//         // 关键：消息泵就在“当前线程”（也就是安装 hook 的线程）跑
//         uint tid = Kernel32.GetCurrentThreadId();

//         // 确保该线程有消息队列（否则 PostThreadMessage 可能失败）
//         User32Hooks.MSG dummy;
//         User32Hooks.PeekMessageW(out dummy, IntPtr.Zero, 0, 0, 0);

//         using var timer = new Timer(_ =>
//         {
//             User32Hooks.PostThreadMessageW(tid, User32Hooks.WM_QUIT, UIntPtr.Zero, IntPtr.Zero);
//         }, null, TimeSpan.FromSeconds(5), Timeout.InfiniteTimeSpan);

//         User32Hooks.MSG msg;
//         while (User32Hooks.GetMessageW(out msg, IntPtr.Zero, 0, 0) > 0)
//         {
//             User32Hooks.TranslateMessage(ref msg);
//             User32Hooks.DispatchMessageW(ref msg);
//         }

//         hook.Stop();
//         Console.WriteLine($"Done. Total KeyDown: {count}");
//         return Task.CompletedTask;
//     }
// }
