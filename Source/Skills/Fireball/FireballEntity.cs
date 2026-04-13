using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FireballEntity : Entity
{
    public Actor Owner;
    public float Speed { get; set; } = 300f;
    public double Angle = 0d;
    public float Damage = 10f;
    public readonly float MaxDuration = 2f;
    public override IHitbox Hitbox =>
        new RectangleHitbox((int)Position.X - 8, (int)Position.Y - 8, 16, 16);

    private FireballGraphicsComponent fireballGraphicsComponent = new();
    private FireballBehaviorComponent fireballBehaviorComponent = new();

    public FireballEntity(Actor owner)
    {
        Owner = owner;
        Game1.World.AddEntity(this);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        fireballGraphicsComponent.Draw(this, spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        fireballGraphicsComponent.Update(gameTime);
        fireballBehaviorComponent.Update(this, gameTime);
    }
}
