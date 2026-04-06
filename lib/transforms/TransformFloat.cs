using System;
using System.Numerics;
using System.Reflection;
using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;

public class TransformFloat: ITransform
{
    private object _obj;

    private PropertyInfo _property;

    private float _from;
    private float _to;
    private double _time;
    private double _length;

    public double Progress => _time / _length;
    public bool IsFinished { get; protected set; }
    public bool IsReady { get; protected set; }
    public event Action OnFinish;

    public TransformFloat(object obj, string propertyName, float to, double length)
    {
        _obj = obj;
        _to = to;
        _length = length;

        Type type = obj.GetType();
        _property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    }

    public void Reset()
    {
        _from = (float)_property.GetValue(_obj);
        _time = 0;
        IsReady = true;
    }

    public void Update(GameTime gameTime)
    {
        if (IsFinished)
            return;

        if (!IsReady)
            throw new Exception("Call reset before starting to update transform.");

        _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_time > _length)
        {
            _time = _length;
            IsFinished = true;
            OnFinish?.Invoke();
        }

        var value = _from + (_to - _from) * (float)Progress;
        _property.SetValue(_obj, value);
    }
}