using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FireballGraphicsComponent
{
    private TextureAsset asset = Assets.Spells.Fireball;
    private int currentFrame = 0;

#pragma warning disable IDE0060
    public void Update(GameTime gameTime)
#pragma warning restore IDE0060
    {
        // TODO: interval
        if (GameState.IsRunning)
        {
            currentFrame++;
            if (currentFrame >= asset.Frames.Count)
                currentFrame = 0;
        }
    }

    public void Draw(FireballEntity fireball, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            asset.Texture,
            new((int)fireball.Position.X, (int)fireball.Position.Y),
            asset.Frames[currentFrame],
            Color.White,
            (float)fireball.Angle,
            new Vector2(asset.Texture.Width / asset.Frames.Count / 2, asset.Texture.Height / 2),
            1f,
            SpriteEffects.None,
            Layer.Entity
        );

        if (GameState.IsDebugMode)
        {
            if (fireball.Hitbox is RectangleHitbox rectangleHitbox)
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
