using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FrozenOrbEntity : Entity
{
    public float Speed { get; set; } = 100f;
    public readonly float MaxDuration = 3f;
    public double Angle = 0d;
    public float Rotation = 0f;
    public override IHitbox Hitbox
        => new RectangleHitbox(0, 0, 0, 0);

    private Actor _owner;
    private FrozenOrbGraphicsComponent _frozenOrbGraphicsComponent = new();
    private FrozenOrbBehaviorComponent _frozenOrbBehaviorComponent = new();

    public FrozenOrbEntity(Actor owner)
    {
        _owner = owner;
        Game1.World.AddEntity(this);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        _frozenOrbGraphicsComponent.Draw(this, spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * 3f;
        _frozenOrbBehaviorComponent.Update(this, gameTime);
    }
}
