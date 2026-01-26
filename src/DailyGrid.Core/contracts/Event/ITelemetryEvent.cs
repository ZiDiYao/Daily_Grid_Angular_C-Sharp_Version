namespace DailyGrid.Core.Contracts.Events;

public interface ITelemetryEvent
{
    DateTimeOffset Timestamp { get; }
}

public readonly record struct KeyDownEvent(DateTimeOffset Timestamp, uint VkCode) : ITelemetryEvent;
