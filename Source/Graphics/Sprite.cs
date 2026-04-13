using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Sprite(Texture2D texture) : IDrawable
{
    public IDrawable? Parent { get; set; }

    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; } = texture.Bounds.Size.ToVector2();
    public Color Color { get; set; } = Color.White;
    public Texture2D Texture { get; set; } = texture;
    public float Rotation { get; set; }
    public Vector2 RelativeOrigin { get; set; } = new(0.5f);
    public bool Hidden { get; set; }

    public DrawNode CreateDrawNode()
        => new SpriteDrawNode(this);
}