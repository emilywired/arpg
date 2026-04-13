using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FrozenOrbSecondaryDrawNode(FrozenOrbSecondaryEntity source) : DrawNode<FrozenOrbSecondaryEntity>(source)
{
    private TextureAsset asset = Assets.Spells.FrozenOrbSecondary;

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (GameState.IsDebugMode)
        {
            if (Source.Hitbox is RectangleHitbox rectangleHitbox)
            {
                spriteBatch.Draw(
                    Assets.RectangleTexture,
                    rectangleHitbox.Bounds with
                    {
                        X = (int)Source.GetDrawPosition().X + rectangleHitbox.Bounds.X,
                        Y = (int)Source.GetDrawPosition().Y + rectangleHitbox.Bounds.Y,
                    },
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

        spriteBatch.Draw(
            asset.Texture,
            new((int)Source.GetDrawPosition().X, (int)Source.GetDrawPosition().Y),
            asset.Frames[0],
            Color.White,
            Source.Angle + MathHelper.ToRadians(90),
            new Vector2(asset.Texture.Width / asset.Frames.Count / 2, asset.Texture.Height / 2),
            1f,
            SpriteEffects.None,
            Layer.Entity
        );
    }
}
