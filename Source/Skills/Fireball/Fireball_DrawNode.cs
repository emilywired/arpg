using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FireballDrawNode(FireballEntity source) : DrawNode<FireballEntity>(source)
{
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Source.Asset.Texture,
            new((int)Source.Position.X, (int)Source.Position.Y),
            Source.Asset.Frames[Source.CurrentFrame],
            Color.White,
            (float)Source.Angle,
            new Vector2(
                Source.Asset.Texture.Width / Source.Asset.Frames.Count / 2,
                Source.Asset.Texture.Height / 2
            ),
            1f,
            SpriteEffects.None,
            Layer.Entity
        );

        if (GameState.IsDebugMode)
        {
            if (Source.Hitbox is RectangleHitbox rectangleHitbox)
            {
                spriteBatch.Draw(
                    Assets.RectangleTexture,
                    rectangleHitbox.Bounds,
                    null,
                    Color.Yellow,
                    0f,
                    Vector2.Zero,
                    SpriteEffects.None,
                    Layer.Hitbox
                );
            }
            else
            {
                throw new NotImplementedException("Unhandled hitbox type");
            }
        }
    }
}
