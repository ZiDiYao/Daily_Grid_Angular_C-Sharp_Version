using DailyGrid.Platform.Windows.dto;

namespace DailyGrid.Platform.Windows.Service.Impl
{
    public sealed class WindowsScreenTimeCounterServiceImple
    {
        private ScreenActivitySample _sample =
            new ScreenActivitySample(DateTime.UtcNow, 0, null, null);

        public int GetIdleTime()
        {
            return 0;
        }

        public DateTime GetLastActiveTimeUtc()
        {
            return DateTime.UtcNow;
        }

        public string? GetCurrActivePage()
        {
            return null;
        }
    }
}
