namespace DailyGrid.Platform.Windows.Hooks;

// Hooks/*Hook.cs 属于封装层（Wrapper/Adapter），它在 Interop 之上：
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using DailyGrid.Platform.Windows.Interop.Win32;

public sealed class LowLevelKeyboardHook : IDisposable
{
    // 1) 必须用字段保存 delegate，防止 GC 回收导致 Hook 随机失效/崩溃
    private readonly User32Hooks.HookProc _proc;

    // 2) Win32 返回的 hook 句柄（HHOOK）
    private IntPtr _hookHandle = IntPtr.Zero;

    // 3) 对外抛回调：按下某个键时触发（vkCode）
    public Action<uint>? OnKeyDown;

    public bool IsRunning => _hookHandle != IntPtr.Zero;

    public LowLevelKeyboardHook()
    {
        _proc = HookCallback;
    }

    // 建议用 PascalCase；你原来的 start/stop 我也保留成别名，避免你其它代码要改
    public void Start()
    {
        if (IsRunning) return;

        // 对于 LL hook：hMod 用当前模块句柄；dwThreadId = 0 表示全局（当前桌面会话）
        IntPtr hMod = Kernel32.GetModuleHandle(null);

        _hookHandle = User32Hooks.SetWindowsHookExW(
            User32Hooks.WH_KEYBOARD_LL,
            _proc,
            hMod,
            0);

        if (_hookHandle == IntPtr.Zero)
        {
            throw new Win32Exception(
                Marshal.GetLastWin32Error(),
                "SetWindowsHookExW(WH_KEYBOARD_LL) failed.");
        }
    }

    public void Stop()
    {
        if (!IsRunning) return;

        // 卸载 hook
        User32Hooks.UnhookWindowsHookEx(_hookHandle);
        _hookHandle = IntPtr.Zero;
    }



    private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        try
        {
            if (nCode >= 0)
            {
                uint msg = unchecked((uint)wParam.ToInt64());

                // 你现在只定义了 WM_KEYDOWN / WM_SYSKEYDOWN，就先只处理按下
                bool isDown = msg == User32Hooks.WM_KEYDOWN || msg == User32Hooks.WM_SYSKEYDOWN;

                if (isDown)
                {
                    // lParam 指向 KBDLLHOOKSTRUCT，我们在这里定义一个最小版本来解析 vkCode
                    var data = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);

                    // 不要在这里做耗时工作（IO/打印/复杂计算），只抛回调或做 O(1) 计数
                    OnKeyDown?.Invoke(data.vkCode);
                }
            }
        }
        catch
        {
            // Hook 回调里不要让异常冒出去，否则可能导致输入异常或 hook 行为不稳定
        }

        // 监听场景：永远传递给下一个 hook
        return User32Hooks.CallNextHookEx(_hookHandle, nCode, wParam, lParam);
    }

    public void Dispose()
    {
        Stop();
        GC.SuppressFinalize(this);
    }

    // 最小键盘结构体（官方：KBDLLHOOKSTRUCT）
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
