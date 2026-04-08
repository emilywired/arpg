using System;
using System.Collections.Generic;

public class ReactiveProperty<T>
{
    private T _value;
    public T Value { 
        get => _value;
        set
        {
            var changed = !_value!.Equals(value);
            _value = value;
            if (changed)
                onChangeTrigger();
        }
    }

    private HashSet<WeakReference<Action>> subscribers = [];

    public event Action OnChange { 
        add => subscribers.Add(new(value));
        remove => subscribers.RemoveWhere(r =>
        {
            if (!r.TryGetTarget(out var handler))
                return true;
                
            return handler == value;
        });
    }

    public ReactiveProperty(T initial)
    {
        _value = initial;
    }

    public void Connect(Action handler, bool triggerNow = true)
    {
        OnChange += handler;
        if (triggerNow)
            handler.Invoke();
    }

    private void onChangeTrigger()
    {
        HashSet<WeakReference<Action>> invalidSubscribers = new();
        foreach (var subscriber in subscribers)
        {
            if (!subscriber.TryGetTarget(out var handler))
            {
                invalidSubscribers.Add(subscriber);
                continue;
            }

            handler.Invoke();
        }

        foreach (var invalidSubscriber in invalidSubscribers)
            subscribers.Remove(invalidSubscriber);
    }
}
