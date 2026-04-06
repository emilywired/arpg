using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HolyFireEntity : IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public IActor Owner;
    public Vector2 Position { get; set; }
    public IHitbox Hitbox => new CircleHitbox(Position, Radius);
    public double Radius = 100d;
    public double Damage = 100d;
    public double SelfDamage = 2d;

    private HolyFireGraphicsComponent _holyFireGraphicsComponent;
    private HolyFireBehaviorComponent _holyFireBehaviorComponent;

    public HolyFireEntity(IActor owner)
    {
        Game1.World.Entities.Add(this);
        Owner = owner;
        _holyFireGraphicsComponent = new(this);
        _holyFireBehaviorComponent = new(this);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _holyFireGraphicsComponent.Draw(this, spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        _holyFireBehaviorComponent.Update(this, gameTime);
    }

    public void Destroy()
    {
        Game1.World.RemoveEntity(this);
        _holyFireBehaviorComponent.Destroy(this);
    }
}
