using Microsoft.Xna.Framework;

public static class IDrawableExtensions
{
    public static Vector2 GetDrawPosition(this IDrawable drawable)
    {
        if (drawable.Parent == null)
            return drawable.Position;

        return drawable.Parent.GetDrawPosition() + drawable.Position;
    }
}