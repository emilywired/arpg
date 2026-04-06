using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class TransformList(IEnumerable<ITransform> transforms) : ITransform
{
    public bool IsFinished { get; protected set; } = false;
    public bool IsReady { get; protected set; } = false;
    public event Action? OnFinish;
    private List<ITransform> transforms = new(transforms);

    public void Reset()
    {
        foreach(var transform in transforms)
        {
            transform.Reset();
        }

        IsReady = true;
    }

    public void Update(GameTime gameTime)
    {
        if (IsFinished)
            return;

        if (!IsReady)
            throw new Exception("Call reset before starting to update transform.");

        foreach(var transform in transforms)
        {
            transform.Update(gameTime);
        }

        transforms.RemoveAll(t => t.IsFinished);
        if (transforms.Count == 0)
        {
            IsFinished = true;
            OnFinish?.Invoke();
        }
    }
}