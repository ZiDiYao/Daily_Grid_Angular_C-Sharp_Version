namespace DailyGrid.Core.Services;

using DailyGrid.Core.Models;
using DailyGrid.Core.Services.Interfaces;


public class addScreenActiveSecondsDeltaServiceImpl : addScreenActiveSecondsDeltaService
{
    // increase the number of screenTime


    public void addScreenActiveSeconds(DailyRecord dailyRecord, int delta)
    {
        dailyRecord.AddScreenActiveSeconds(delta);
    }
}