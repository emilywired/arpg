using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

public abstract class Entity : IDrawable
{
    public IDrawable? Parent { get; set; }

    public string Id { get; } = Guid.NewGuid().ToString();
    public abstract IHitbox Hitbox { get; }
    public Vector2 Position { get; set; }

    public bool IsDestroyed { get; private set; }

    public bool Hidden { get; }

    protected List<IDrawable> drawables = [];

    public virtual void Update(GameTime gameTime) { }

    protected void AddDrawable(IDrawable drawable)
    {
        drawables.Add(drawable);
        drawable.Parent = this;
    }

    protected virtual IEnumerable<DrawNode> CreateCompositeDrawNodes()
        => drawables.Where(drawable => !drawable.Hidden).Select(drawable => drawable.CreateDrawNode());

    public DrawNode CreateDrawNode()
    {
        IEnumerable<DrawNode> drawableNodes = CreateCompositeDrawNodes();
        return new CompositeDrawNode(this, drawableNodes);
    }

    public virtual void Destroy()
    {
        IsDestroyed = true;
    }
}