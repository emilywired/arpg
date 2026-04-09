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

    private HashSet<WeakReference<Action<T>>> subscribers = [];

    public event Action<T> OnChange { 
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

    public void Connect(Action<T> handler, bool triggerNow = true)
    {
        OnChange += handler;
        if (triggerNow)
            handler.Invoke(_value);
    }

    public void Connect(Action handler, bool triggerNow = true)
        => Connect(_ => handler(), triggerNow);

    private void onChangeTrigger()
    {
        HashSet<WeakReference<Action<T>>> invalidSubscribers = [];
        foreach (var subscriber in subscribers)
        {
            if (!subscriber.TryGetTarget(out var handler))
            {
                invalidSubscribers.Add(subscriber);
                continue;
            }

            handler.Invoke(_value);
        }

        foreach (var invalidSubscriber in invalidSubscribers)
            subscribers.Remove(invalidSubscriber);
    }
}
