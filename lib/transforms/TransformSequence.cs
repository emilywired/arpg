using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

public class TransformSequence(IEnumerable<ITransform> transforms) : ITransform
{
    public bool IsFinished { get; protected set; } = false;
    public bool IsReady { get; protected set; }
    public event Action? OnFinish;
    private List<ITransform> transforms = new(transforms);

    public void Reset()
    {
        // Do it in update.
        IsReady = true;
    }

    public void Update(GameTime gameTime)
    {
        if (IsFinished)
            return;

        if (!IsReady)
            throw new Exception("Call reset before starting to update transform.");

        if (transforms.FirstOrDefault() is ITransform transform)
        {
            if (!transform.IsReady)
                transform.Reset();

            transform.Update(gameTime);
            if (transform.IsFinished)
                transforms.Remove(transform);
        }

        if (transforms.Count == 0)
        {
            IsFinished = true;
            OnFinish?.Invoke();
        }
    }
}