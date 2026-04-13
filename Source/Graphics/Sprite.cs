using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Sprite(Texture2D texture) : IDrawable
{
    public IDrawable? Parent { get; set; }

    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public Color Color { get; set; } = Color.White;
    public Texture2D Texture { get; set; } = texture;
    public float Rotation { get; set; }

    public DrawNode CreateDrawNode()
        => new SpriteDrawNode(this);
}