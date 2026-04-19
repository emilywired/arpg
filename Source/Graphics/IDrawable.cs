using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IDrawable
{
    IDrawable? Parent { get; set; }
    Vector2 Position { get; }
    bool Hidden { get; }

    void Draw(SpriteBatch spriteBatch);
}