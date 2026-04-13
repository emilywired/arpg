using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpriteDrawNode(Sprite source) : DrawNode<Sprite>(source)
{
    private Vector2 position = source.GetDrawPosition();
    private Vector2 size = source.Size;

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Source.Texture,
            new(position.ToPoint(), size.ToPoint()),
            null,
            Source.Color,
            Source.Rotation,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.Hitbox
        );
    }
}