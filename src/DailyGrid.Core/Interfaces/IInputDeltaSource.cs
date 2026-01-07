namespace DailyGrid.Core.Interfaces;

public readonly record struct InputDelta(
    int ScreenActiveSecondsDelta,
    int MouseClicksDelta,
    int KeyPressesDelta
);

// Windows 层只负责“告诉增量”，Core 决定怎么用
public interface IInputDeltaSource
{
    InputDelta ReadDelta(DateTime now);
}
