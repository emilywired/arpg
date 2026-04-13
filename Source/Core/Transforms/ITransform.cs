using System;
using Microsoft.Xna.Framework;

public interface ITransform
{
    bool IsFinished { get; }
    bool IsReady { get; }
    event Action? OnFinish;

    void Reset();
    void Update(GameTime gameTime);
}