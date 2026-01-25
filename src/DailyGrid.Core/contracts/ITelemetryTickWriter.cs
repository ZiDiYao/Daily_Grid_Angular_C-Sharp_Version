namespace DailyGrid.Core.contracts;

public interface ITelemetryTickWriter
{
    bool TryWrite(TelemetryTick tick);
}