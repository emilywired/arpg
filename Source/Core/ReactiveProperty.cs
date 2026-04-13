using System;
using System.Collections.Generic;

public class ReactiveProperty<T>
{
    private T _value;
    public T Value
    {
        get => _value;
        set
        {
            bool changed = !_value!.Equals(value);
            _value = value;
            if (changed)
                onChangeTrigger();
        }
    }

    private HashSet<Subscription> subscriptions = [];

    public ReactiveProperty(T initial)
    {
        _value = initial;
    }

    public void Connect(object owner, Action<T> handler, bool triggerNow = true)
    {
        var subscription = new Subscription(owner, handler);
        _ = subscriptions.Add(subscription);

        if (triggerNow)
            handler.Invoke(_value);
    }

    public void Connect(object owner, Action handler, bool triggerNow = true)
        => Connect(owner, _ => handler(), triggerNow);

    private void onChangeTrigger()
    {
        HashSet<Subscription> invalidSubscribers = [];
        foreach (Subscription sub in subscriptions)
        {
            if (!sub.Owner.IsAlive)
            {
                _ = invalidSubscribers.Add(sub);
                continue;
            }

            sub.Handler.Invoke(_value);
        }

        foreach (Subscription invalidSubscriber in invalidSubscribers)
            _ = subscriptions.Remove(invalidSubscriber);
    }

    private class Subscription(object owner, Action<T> handler)
    {
        public WeakReference Owner = new(owner);
        public Action<T> Handler = handler;
    }
}
