using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public static class MouseManager
{
    public static Vector2 RawMousePosition
        => Mouse.GetState().Position.ToVector2();

    public static Vector2 ScreenMousePosition
        => RawMousePosition / Game1.Config.Scale;

    public static Vector2 WorldMousePosition
        => Camera.ScreenToWorld(ScreenMousePosition);
}
