namespace DailyGrid.Platform.Windows.dto
{

    public sealed record ScreenActivitySample(
    DateTime TimestampUtc,
    int IdleMilliseconds,
    string? ForegroundProcessName,
    string? ForegroundWindowTitle
);

}

