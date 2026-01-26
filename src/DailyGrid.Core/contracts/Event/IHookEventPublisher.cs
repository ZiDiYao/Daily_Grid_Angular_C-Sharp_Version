namespace DailyGrid.Core.Contracts.Events;

using System;
using System.Reactive.Subjects;
using DailyGrid.Core.Contracts.Events;

public interface IHookEventPublisher
{
    void Publish(ITelemetryEvent evt);
    IObservable<ITelemetryEvent> Events { get; }
}

