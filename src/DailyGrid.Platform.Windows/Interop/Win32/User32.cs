namespace DailyGrid.Platform.Windows.Interop.Win32;

using System;
using System.Runtime.InteropServices;
using System.Text;

internal static class User32
{
    [DllImport("user32.dll", SetLastError = true)]
    internal static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern int GetWindowTextW(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [StructLayout(LayoutKind.Sequential)]
    internal struct LASTINPUTINFO
    {
        public uint cbSize;
        public uint dwTime;
    }

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
}
