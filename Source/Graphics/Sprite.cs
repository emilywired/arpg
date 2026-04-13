using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Sprite(Texture2D texture) : IDrawable
{
    public Vector2 Position { get; set; }
    public Color Color { get; set; }
    public Texture2D Texture { get; set; } = texture;
    public float Rotation { get; set; }

    public DrawNode CreateDrawNode()
        => new SpriteDrawNode(this);
}