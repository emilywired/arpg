
using System;
using Microsoft.Xna.Framework;

public static class Vector2ExtensionMethods
{
    public static float DistanceTo(this Vector2 a, Vector2 b)
        => Vector2.Distance(a, b);

    public static float AngleTo(this Vector2 a, Vector2 b)
        => MathF.Atan2(
            b.Y - a.Y,
            b.X - a.X
        );
}