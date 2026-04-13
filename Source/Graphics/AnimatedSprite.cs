using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class AnimatedSprite
{
    private int currentFrame = 0;
    private float elapsedTime = 0f;
    protected float FrameTime { get; set; } = 0.1f;
    public TextureAsset Asset { get; private set; } = null!;

    public SpriteEffects SpriteEffects { get; set; }
    public Vector2 Position { get; set; }

    public void SetTextureAsset(TextureAsset asset)
    {
        Asset = asset;
        Reset();
    }

    public virtual void Update(GameTime gameTime)
    {
        elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (elapsedTime >= FrameTime)
        {
            elapsedTime = 0f;

            int nextFrame = currentFrame + 1;
            if (Asset.Looping)
                nextFrame %= Asset.Frames.Count;
            else
                nextFrame = Math.Min(nextFrame, Asset.Frames.Count - 1);
            currentFrame = nextFrame;
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Rectangle frame = Asset.Frames[currentFrame];

        spriteBatch.Draw(
            Asset.Texture,
            new((int)Position.X, (int)Position.Y),
            frame,
            Color.White,
            0f,
            frame.Size.ToVector2() / 2,
            1f,
            SpriteEffects,
            Layer.Player
        );
    }

    public virtual void Reset()
    {
        currentFrame = 0;
        elapsedTime = 0;
    }
}