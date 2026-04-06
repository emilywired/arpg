using System;
using Microsoft.Xna.Framework;

public abstract class Transform
{
    public bool IsFinished { get; protected set; } = false;
    public abstract event Action OnFinish;
    public abstract double Progress { get; }
    
    public abstract void Update(GameTime gameTime);
}