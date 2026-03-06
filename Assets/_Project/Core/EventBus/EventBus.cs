using System;
using System.Collections.Generic;

namespace Project.Core.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, Delegate> subscribers = new();

        public void Subscribe<T>(Action<T> handler)
        {
            var type = typeof(T);

            if (subscribers.TryGetValue(type, out var existing))
            {
                subscribers[type] = Delegate.Combine(existing, handler);
            }
            else
            {
                subscribers[type] = handler;
            }
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            var type = typeof(T);

            if (!subscribers.TryGetValue(type, out var existing))
                return;

            var current = Delegate.Remove(existing, handler);

            if (current == null)
                subscribers.Remove(type);
            else
                subscribers[type] = current;
        }

        public void Publish<T>(T signal)
        {
            var type = typeof(T);

            if (subscribers.TryGetValue(type, out var del))
            {
                ((Action<T>)del)?.Invoke(signal);
            }
        }
    }
}
