using Microsoft.Xna.Framework;

public interface IDrawable
{
    IDrawable? Parent { get; set; }
    Vector2 Position { get; }

    DrawNode CreateDrawNode();
}