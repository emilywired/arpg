using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpriteDrawNode(Sprite source) : DrawNode<Sprite>(source)
{
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Source.Texture,
            new(Source.Position.ToPoint(), new(64, 64)),
            null,
            Source.Color,
            Source.Rotation,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.Hitbox
        );
    }
}