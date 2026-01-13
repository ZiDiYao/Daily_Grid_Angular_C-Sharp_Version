namespace DailyGrid.Core.contracts;

// 定义 Contract 关于一切 Core 的业务需要使用的信息，比如 点击次数，滚轮等
// 需要是最简洁的，Windows 不应该对其有过多的 逻辑加工，其属于 Core 的范围
// MVP
public sealed record MouseSample(
        int ClickDelta = 0,
        double MoveDistancePxDelta = 0

);