namespace DailyGrid.Core.Services;

using DailyGrid.Core.Models;
using DailyGrid.Core.Services.Interfaces;

public class addKeyPressesDeltaServiceImpl : addKeyPressesDeltaService
{
    public void addKeyPressesDelta(DailyRecord dailyRecord, int delta)
    {
        // get the delta about presses then unpackage the data 
        dailyRecord.AddKeyPresses(delta);

    }


}