using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FireballGraphicsComponent
{
    private TextureAsset _asset = Assets.Spells.Fireball;
    private int _currentFrame = 0;

    public void Update(GameTime gameTime)
    {
        // TODO: interval
        _currentFrame++;
        if (_currentFrame >= _asset.Frames.Count)
            _currentFrame = 0;
    }

    public void Draw(FireballEntity fireball, SpriteBatch spriteBatch)
    {
        if (GameState.IsRunning)
        {
            _currentFrame++;
            if (_currentFrame >= _asset.Frames.Count)
                _currentFrame = 0;
        }

        spriteBatch.Draw(
            _asset.Texture,
            new((int)fireball.Position.X, (int)fireball.Position.Y),
            _asset.Frames[_currentFrame],
            Color.White,
            (float)fireball.Angle,
            new Vector2(_asset.Texture.Width / _asset.Frames.Count / 2, _asset.Texture.Height / 2),
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
