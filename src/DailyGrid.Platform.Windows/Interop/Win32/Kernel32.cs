namespace DailyGrid.Platform.Windows.Interop.Win32;

using System;
using System.Runtime.InteropServices;

internal static class Kernel32
{
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern IntPtr GetModuleHandle(string? lpModuleName);

    [DllImport("kernel32.dll")]
    internal static extern uint GetCurrentThreadId();
}
