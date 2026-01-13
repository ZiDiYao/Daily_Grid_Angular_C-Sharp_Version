namespace DailyGrid.Platform.Windows.Hooks;

// 所以的 Hook 都应该 implement IHOOK
// HOOK 的线程开启和释放必须是可控的，需要手动结束
public interface IHook : IDisposable
{
    bool IsRunning { get; }

    string Name { get; }

    void Start();
    void Stop();
}