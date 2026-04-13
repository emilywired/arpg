using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FrozenOrbSecondaryEntity : Entity
{
    public float Speed { get; set; } = 150f;
    public float Angle = 0f;
    public float Damage = 5f;
    public readonly float MaxDuration = 1.5f;
    public override IHitbox Hitbox =>
        new RectangleHitbox((int)Position.X - 16, (int)Position.Y - 16, 32, 32);

    private FrozenOrbSecondaryGraphicsComponent frozenOrbSecondaryGraphicsComponent = new();
    private FrozenOrbSecondaryBehaviorComponent frozenOrbSecondaryBehaviorComponent = new();

    public FrozenOrbSecondaryEntity()
    {
        Game1.World.AddEntity(this);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        frozenOrbSecondaryGraphicsComponent.Draw(this, spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        frozenOrbSecondaryBehaviorComponent.Update(this, gameTime);
    }
}
