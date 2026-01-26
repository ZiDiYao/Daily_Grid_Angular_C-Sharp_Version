using System;
using System.Reactive.Subjects;
using DailyGrid.Core.Contracts.Events;

namespace DailyGrid.Platform.Windows.Hooks;

public sealed class HookEventPublisher : IHookEventPublisher, IDisposable
{
    private readonly Subject<ITelemetryEvent> _subject = new();

    public IObservable<ITelemetryEvent> Events => _subject;

    public void Publish(ITelemetryEvent evt) => _subject.OnNext(evt);

    public void Dispose()
    {
        _subject.OnCompleted();
        _subject.Dispose();
    }
}
