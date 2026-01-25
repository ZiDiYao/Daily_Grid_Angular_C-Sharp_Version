namespace DailyGrid.Core.contracts;

public interface ITelemetryTickReader
{
    IAsyncEnumerable<TelemetryTick> ReadAllAsync(CancellationToken ct);
}