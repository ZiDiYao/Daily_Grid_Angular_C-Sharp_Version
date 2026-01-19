using DailyGrid.Core.Models;

namespace DailyGrid.Core.Services.Interfaces;


public interface addScreenActiveSecondsDeltaService
{
    void addScreenActiveSeconds(DailyRecord dailyRecord, int delta);


}