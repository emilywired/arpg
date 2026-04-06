using System;
using System.Numerics;
using System.Reflection;
using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;

public class TransformVector2 : Transform
{
    private object _obj;

    private PropertyInfo _property;

    private Vector2 _from;
    private Vector2 _to;
    private double _time;
    private double _length;

    public override double Progress => _time / _length;
    public override event Action OnFinish;

    public TransformVector2(object obj, string propertyName, Vector2 from, Vector2 to, double length)
    {
        _obj = obj;

        _from = from;
        _to = to;

        _time = 0;
        _length = length;

        Type type = obj.GetType();
        _property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    }

    public override void Update(GameTime gameTime)
    {
        if (IsFinished)
            return;

        _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_time > _length)
        {
            _time = _length;
            IsFinished = true;
            OnFinish?.Invoke();
        }

        var value = (_to - _from) + _from * (float)Progress;
        _property.SetValue(_obj, value);
    }
}