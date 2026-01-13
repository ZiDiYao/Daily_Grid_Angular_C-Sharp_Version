namespace DailyGrid.Core.contracts;

public sealed record ScreenSample(
    bool IsUserPresent = true,
    bool IsForegroundEligible = true,
    string AppId = "",
    string? WindowTitle = null
);

