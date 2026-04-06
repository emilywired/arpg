using System.Numerics;
using arpg;
using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;

public static class Camera
{
    public static Matrix Transform { get; private set; }
    public static Vector2 Origin { get; private set; }
    public static Vector2 Offset { get; private set; }

    public static Vector2 ScreenToWorld(Vector2 screen)
        => Origin + screen;

    public static void Follow(IActor actor)
    {
        Matrix position = Matrix.CreateTranslation(
            -(int)actor.Position.X,
            -(int)actor.Position.Y,
            0
        );

        Matrix offset = Matrix.CreateTranslation(
            Game1.NativeResolution.Width / 2,
            Game1.NativeResolution.Height / 2,
            0
        );
        Transform = position * offset;

        Offset = new(-Game1.NativeResolution.Width / 2, -Game1.NativeResolution.Height / 2);

        Origin = new(
            actor.Position.X - Game1.NativeResolution.Width / 2,
            actor.Position.Y - Game1.NativeResolution.Height / 2
        );
    }
}
