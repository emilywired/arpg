using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class AnimatedSprite : IDrawable
{
    private float elapsedTime = 0f;
    protected float FrameTime { get; set; } = 0.1f;
    public TextureAsset Asset { get; private set; } = null!;
    public int CurrentFrame { get; private set; }

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

            int nextFrame = CurrentFrame + 1;
            if (Asset.Looping)
                nextFrame %= Asset.Frames.Count;
            else
                nextFrame = Math.Min(nextFrame, Asset.Frames.Count - 1);
            CurrentFrame = nextFrame;
        }
    }

    public virtual void Reset()
    {
        CurrentFrame = 0;
        elapsedTime = 0;
    }

    public DrawNode CreateDrawNode()
        => new AnimatedSpriteDrawNode(this);
}