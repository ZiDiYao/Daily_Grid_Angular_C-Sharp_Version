namespace DailyGrid.Platform.Windows.eventing;

using System.Threading.Channels;
using DailyGrid.Core.contracts;

// 负责发布信息
// 开启多线程，避免堵塞主线程

public sealed class DeltaPublisher : IAsyncDisposable
{


    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }

    // constructor
    public DeltaPublisher()
    {
        
    }

    public void tryPublish()
    {
        
    }

    // send loop async
}