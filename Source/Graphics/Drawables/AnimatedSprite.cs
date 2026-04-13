using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class AnimatedSprite(TextureAsset? initialAsset = null) : IDrawable, IUpdateable
{
    public IDrawable? Parent { get; set; }

    private float elapsedTime = 0f;
    protected float FrameTime { get; set; } = 0.1f;
    public TextureAsset Asset { get; private set; } = initialAsset!;
    public int CurrentFrame { get; private set; }

    public SpriteEffects SpriteEffects { get; set; }
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public Vector2 RelativeOrigin { get; set; } = new(0.5f);
    public bool Hidden { get; set; }

    public void SetTextureAsset(TextureAsset asset)
    {
        Asset = asset;
        Reset();
    }

    public virtual void Update(float dt)
    {
        elapsedTime += dt;

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