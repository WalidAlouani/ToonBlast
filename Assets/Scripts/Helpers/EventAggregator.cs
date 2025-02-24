using System.Collections.Generic;
using System;

public class EventAggregator : Singleton<EventAggregator>, IEventAggregator
{
    private readonly Dictionary<Type, List<Delegate>> _subscribers = new Dictionary<Type, List<Delegate>>();

    public void Subscribe<TEvent>(Action<TEvent> handler)
    {
        if (!_subscribers.TryGetValue(typeof(TEvent), out var handlers))
        {
            handlers = new List<Delegate>();
            _subscribers[typeof(TEvent)] = handlers;
        }
        handlers.Add(handler);
    }

    public void Unsubscribe<TEvent>(Action<TEvent> handler)
    {
        if (_subscribers.TryGetValue(typeof(TEvent), out var handlers))
        {
            handlers.Remove(handler);
        }
    }

    public void Publish<TEvent>(TEvent eventItem)
    {
        if (_subscribers.TryGetValue(typeof(TEvent), out var handlers))
        {
            foreach (Action<TEvent> handler in handlers)
            {
                handler?.Invoke(eventItem);
            }
        }
    }
}
