using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class AnimatedSpriteDrawNode(AnimatedSprite source) : DrawNode<AnimatedSprite>(source)
{
    private Vector2 position = source.GetDrawPosition();
    private Texture2D texture = source.Asset.Texture;
    private Rectangle frame = source.Asset.Frames[source.CurrentFrame];
    private SpriteEffects spriteEffects = source.SpriteEffects;
    private float rotation = source.Rotation;

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            texture,
            new((int)position.X, (int)position.Y),
            frame,
            Color.White,
            rotation,
            Source.RelativeOrigin * frame.Size.ToVector2(),
            1f,
            spriteEffects,
            Layer.Player
        );
    }
}