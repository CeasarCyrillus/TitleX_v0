using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Events
{
    public class EventBus
    {
        private readonly ConcurrentDictionary<Type, ConcurrentBag<Delegate>> _handlers = new ();
    
        private static EventBus _instance;
        public static EventBus Instance
        {
            get { return _instance ??= new EventBus(); }
        }

        public void Subscribe<T>(Func<T, Task> handler)
        {
            var eventType = typeof(T);
            var bag = _handlers.GetOrAdd(eventType, _ => new ConcurrentBag<Delegate>());
            bag.Add(handler);
        }

        public async Task Publish<T>(T @event)
        {
            var eventType = typeof(T);
            if (_handlers.TryGetValue(eventType, out var bag))
            {
                foreach (var handler in bag)
                {
                    if (handler is Func<T, Task> typedHandler)
                    {
                        try
                        {
                            await typedHandler(@event).ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError(ex);
                        }
                    }
                }
            }
        }

        public void Unsubscribe<T>(Func<T, Task> handler)
        {
            var eventType = typeof(T);
            if (_handlers.TryGetValue(eventType, out var bag))
            {
                bag.TryTake(out var _); // Ensure the bag is initialized
                bag = new ConcurrentBag<Delegate>(bag.Where(d => d != handler));
                _handlers[eventType] = bag;
            }
        }
    }
}
