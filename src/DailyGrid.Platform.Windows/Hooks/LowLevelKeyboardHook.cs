namespace DailyGrid.Platform.Windows.Hooks;

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using DailyGrid.Core.Contracts.Events;
using DailyGrid.Platform.Windows.Interop.Win32;

public sealed class LowLevelKeyboardHook : IHook
{
    private readonly User32Hooks.HookProc _proc;
    private IntPtr _hookHandle = IntPtr.Zero;

    private IHookEventPublisher? _publisher;

    public bool IsRunning => _hookHandle != IntPtr.Zero;
    public string Name => "LowLevelKeyboard";

    public LowLevelKeyboardHook()
    {
        _proc = HookCallback;
    }

    public void AttachPublisher(IHookEventPublisher publisher)
        => _publisher = publisher;

    public void Start()
    {
        if (IsRunning) return;

        IntPtr hMod = Kernel32.GetModuleHandle(null);

        _hookHandle = User32Hooks.SetWindowsHookExW(
            User32Hooks.WH_KEYBOARD_LL,
            _proc,
            hMod,
            0);

        if (_hookHandle == IntPtr.Zero)
            throw new Win32Exception(Marshal.GetLastWin32Error(),
                "SetWindowsHookExW(WH_KEYBOARD_LL) failed.");
    }

    public void Stop()
    {
        if (!IsRunning) return;

        User32Hooks.UnhookWindowsHookEx(_hookHandle);
        _hookHandle = IntPtr.Zero;
    }

    private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        try
        {
            if (nCode >= 0 && _publisher != null)
            {
                uint msg = unchecked((uint)wParam.ToInt64());
                bool isDown = msg == User32Hooks.WM_KEYDOWN || msg == User32Hooks.WM_SYSKEYDOWN;

                if (isDown)
                {
                    var data = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);
                    _publisher.Publish(new KeyDownEvent(DateTimeOffset.UtcNow, data.vkCode));
                }
            }
        }
        catch { }

        return User32Hooks.CallNextHookEx(_hookHandle, nCode, wParam, lParam);
    }

    public void Dispose()
    {
        try { Stop(); } catch { }
        GC.SuppressFinalize(this);
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct KBDLLHOOKSTRUCT
    {
        public uint vkCode;
        public uint scanCode;
        public uint flags;
        public uint time;
        public UIntPtr dwExtraInfo;
    }
}
