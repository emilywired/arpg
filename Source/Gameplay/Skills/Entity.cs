using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class Entity
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public abstract IHitbox Hitbox { get; }
    public Vector2 Position { get; set; }

    public bool IsDestroyed { get; private set; }

    public virtual void Update(GameTime gameTime) {}
    public virtual void Draw(SpriteBatch spriteBatch) {}

    public virtual void Destroy()
    {
        IsDestroyed = true;
    }
}