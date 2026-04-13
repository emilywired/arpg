using System;
using System.Reflection;
using Microsoft.Xna.Framework;

public class TransformFloat : ITransform
{
    private object obj;

    private PropertyInfo property;

    private float from;
    private float to;
    private double time;
    private double length;

    public double Progress => time / length;
    public bool IsFinished { get; protected set; }
    public bool IsReady { get; protected set; }
    public event Action? OnFinish;

    public TransformFloat(object _obj, string _propertyName, float _to, double _length)
    {
        obj = _obj;
        to = _to;
        length = _length;

        Type type = obj.GetType();
        property = type.GetProperty(
            _propertyName,
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
        )!;
    }

    public void Reset()
    {
        from = (float)property.GetValue(obj)!;
        time = 0;
        IsReady = true;
    }

    public void Update(GameTime gameTime)
    {
        if (IsFinished)
            return;

        if (!IsReady)
            throw new Exception("Call reset before starting to update transform.");

        time += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (time > length)
        {
            time = length;
            IsFinished = true;
            OnFinish?.Invoke();
        }

        float value = from + ((to - from) * (float)Progress);
        property.SetValue(obj, value);
    }
}
