namespace DailyGrid.Core.contracts;

/// <summary>
/// TelemetryTick is a platform-agnostic snapshot emitted periodically by the platform adapter (e.g., Windows).
/// Semantics:
/// - The tick covers the half-open time range [StartUtc, EndUtc).
/// - Samples contain DELTAS accumulated within this range, except ScreenSample which is a snapshot/context.
/// - The adapter should reset counters after emitting a tick.
/// </summary>
public sealed record TelemetryTick(
    DateTimeOffset StartUtc,
    DateTimeOffset EndUtc,
    KeyboardSample Keyboard,
    MouseSample Mouse,
    ScreenSample Screen
)
{
    public TimeSpan Interval => EndUtc - StartUtc;
}
