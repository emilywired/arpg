using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FrozenOrbEntity : Entity
{
    public float Speed { get; set; } = 100f;
    public readonly float MaxDuration = 3f;
    public double Angle = 0d;
    public float Rotation = 0f;
    public override IHitbox Hitbox => new RectangleHitbox(0, 0, 0, 0);

    private FrozenOrbGraphicsComponent frozenOrbGraphicsComponent = new();
    private FrozenOrbBehaviorComponent frozenOrbBehaviorComponent = new();

    public FrozenOrbEntity()
    {
        Game1.World.AddEntity(this);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        frozenOrbGraphicsComponent.Draw(this, spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * 3f;
        frozenOrbBehaviorComponent.Update(this, gameTime);
    }
}
