using System;
using Microsoft.Xna.Framework;

public static class MathUtils
{
    public static Vector2 ClosestEdgeOfCircle(Vector2 center, float radius, Vector2 point)
        => ClosestEdgeOfCircle(center, radius, center.AngleTo(point));

    public static Vector2 ClosestEdgeOfCircle(Vector2 center, float radius, float angle)
        => new(
            (radius * MathF.Cos(angle)) + center.X,
            (radius * MathF.Sin(angle)) + center.Y
        );
}