using System;

public interface ITransform
{
    bool IsFinished { get; }
    bool IsReady { get; }
    event Action? OnFinish;

    void Reset();
    void Update(float dt);
}