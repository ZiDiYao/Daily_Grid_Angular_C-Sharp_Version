namespace DailyGrid.Platform.Windows.Service.Impl;

public interface IScreenTimeCounterService
{
    /// <summary>
    /// 去 get 到 IDLE 时间，然后由 Core module 去调用
    /// </summary>
    int GetIdleMilliseconds();

}

