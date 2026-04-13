using System;
using Microsoft.Xna.Framework;

public interface ITransform
{
    public bool IsFinished { get; }
    public bool IsReady { get; }
    public event Action? OnFinish;

    public void Reset();
    public void Update(GameTime gameTime);
}