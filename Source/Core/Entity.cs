using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class Entity : IDrawable, IUpdateable
{
    public IDrawable? Parent { get; set; }

    public string Id { get; } = Guid.NewGuid().ToString();
    public abstract IHitbox Hitbox { get; }
    public Vector2 Position { get; set; }

    public bool IsDestroyed { get; private set; }

    public bool Hidden { get; }

    protected List<IDrawable> drawables = [];

    public virtual void Update(float dt)
    {
        foreach (IUpdateable updateables in drawables.OfType<IUpdateable>())
        {
            updateables.Update(dt);
        }
    }

    protected void AddDrawable(IDrawable drawable)
    {
        drawables.Add(drawable);
        drawable.Parent = this;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (IDrawable drawable in drawables)
        {
            if (!drawable.Hidden)
            {
                drawable.Draw(spriteBatch);
            }
        }
    }

    public virtual void Destroy()
    {
        IsDestroyed = true;
    }
}