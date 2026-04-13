using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HolyFireEntity : Entity
{
    public Actor Owner;
    public override IHitbox Hitbox
        => new CircleHitbox(Position, Radius);
    public double Radius = 100d;
    public double Damage = 100d;
    public double SelfDamage = 2d;

    private HolyFireGraphicsComponent _holyFireGraphicsComponent;
    private HolyFireBehaviorComponent _holyFireBehaviorComponent;

    public HolyFireEntity(Actor owner)
    {
        Game1.World.AddEntity(this);
        Owner = owner;
        _holyFireGraphicsComponent = new(this);
        _holyFireBehaviorComponent = new(this);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        _holyFireGraphicsComponent.Draw(this, spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        _holyFireBehaviorComponent.Update(this, gameTime);
    }

    public override void Destroy()
    {
        base.Destroy();
        _holyFireBehaviorComponent.Destroy(this);
    }
}
