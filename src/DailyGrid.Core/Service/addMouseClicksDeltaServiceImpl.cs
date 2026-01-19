using DailyGrid.Core.Models;
using DailyGrid.Core.Services.Interfaces;
public class addMouseClicksDeltaServiceImpl : addMouseClicksDeltaService
{
    public void addMouseClicks(DailyRecord dailyRecord, int delta)
    {
        dailyRecord.AddMouseClicks(delta);
    }
}
