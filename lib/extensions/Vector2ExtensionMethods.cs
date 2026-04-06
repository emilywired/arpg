
using Microsoft.Xna.Framework;

public static class Vector2ExtensionMethods
{
    public static float DistanceTo(this Vector2 a, Vector2 b)
        => Vector2.Distance(a, b);
}