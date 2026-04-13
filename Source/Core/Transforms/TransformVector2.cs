using System;
using System.Reflection;
using Vector2 = Microsoft.Xna.Framework.Vector2;

public class TransformVector2 : ITransform
{
    private object obj;

    private PropertyInfo property;

    private Vector2? from;
    private Vector2 to;
    private double time;
    private double length;

    public double Progress => time / length;
    public bool IsFinished { get; protected set; }
    public bool IsReady { get; protected set; }
    public event Action? OnFinish;

    public TransformVector2(object _obj, string _propertyName, Vector2 _to, double _length)
    {
        obj = _obj;
        to = _to;
        length = _length;

        Type type = _obj.GetType();
        property = type.GetProperty(
            _propertyName,
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
        )!;
    }

    public void Reset()
    {
        from = (Vector2)property.GetValue(obj)!;
        time = 0;
        IsReady = true;
    }

    public void Update(float dt)
    {
        if (IsFinished)
            return;

        if (!IsReady)
            throw new Exception("Call reset before starting to update transform.");

        time += dt;
        if (time > length)
        {
            time = length;
            IsFinished = true;
            OnFinish?.Invoke();
        }

        Vector2? value = from + ((to - from) * (float)Progress);
        property.SetValue(obj, value);
    }
}
