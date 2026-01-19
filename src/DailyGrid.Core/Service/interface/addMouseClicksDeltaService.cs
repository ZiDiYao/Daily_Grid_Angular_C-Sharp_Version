using DailyGrid.Core.Models;

namespace DailyGrid.Core.Services.Interfaces;

public interface addMouseClicksDeltaService
{

    void addMouseClicks(DailyRecord dailyRecord, int delta);
    
    
    
}