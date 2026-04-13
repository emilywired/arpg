using System;
using Microsoft.Xna.Framework;

public interface IHitbox
{
    bool Intersects(IHitbox other);
}

// TODO: refactor
public class RectangleHitbox(int x, int y, int width, int height) : IHitbox
{
    public Rectangle Bounds { get; set; } = new(x, y, width, height);

    public bool Intersects(IHitbox other)
    {
        return other switch
        {
            RectangleHitbox rectangleHitbox => Intersects(rectangleHitbox),
            CircleHitbox circleHitbox => Intersects(circleHitbox),
            _ => throw new NotImplementedException(),
        };
    }

    public bool Intersects(RectangleHitbox rectangleHitbox)
    {
        return Bounds.Intersects(rectangleHitbox.Bounds);
    }

    public bool Intersects(CircleHitbox circleHitbox)
    {
        float closestX = Math.Clamp(circleHitbox.Center.X, Bounds.Left, Bounds.Right);
        float closestY = Math.Clamp(circleHitbox.Center.Y, Bounds.Top, Bounds.Bottom);
        float distanceX = circleHitbox.Center.X - closestX;
        float distanceY = circleHitbox.Center.Y - closestY;
        return (distanceX * distanceX + distanceY * distanceY)
            <= (circleHitbox.Radius * circleHitbox.Radius);
    }
}

public class CircleHitbox(Vector2 center, double radius) : IHitbox
{
    public Vector2 Center = center;
    public double Radius = radius;

    public bool Intersects(IHitbox other)
    {
        return other switch
        {
            RectangleHitbox rectangleHitbox => Intersects(rectangleHitbox),
            CircleHitbox circleHitbox => Intersects(circleHitbox),
            _ => throw new NotImplementedException(),
        };
    }

    public bool Intersects(CircleHitbox circle)
    {
        double x1 = Center.X;
        double y1 = Center.Y;
        double x2 = circle.Center.X;
        double y2 = circle.Center.Y;
        double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        return distance <= Radius + circle.Radius;
    }

    public bool Intersects(Rectangle rectangle)
    {
        float closestX = Math.Clamp(Center.X, rectangle.Left, rectangle.Right);
        float closestY = Math.Clamp(Center.Y, rectangle.Top, rectangle.Bottom);
        float distanceX = Center.X - closestX;
        float distanceY = Center.Y - closestY;
        return (distanceX * distanceX + distanceY * distanceY) <= (Radius * Radius);
    }
}
