using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Skeleton : IMonster
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public ActorKind Kind { get; } = ActorKind.Monster;

    public ReactiveProperty<ActorState> State { get; } = new(ActorState.Idling);
    ActorState IActor.State => State.Value;

    public ReactiveProperty<ActorActionState> ActionState = new(ActorActionState.None);
    ActorActionState IActor.ActionState => ActionState.Value;

    public ActorFacing Facing { get; set; } = ActorFacing.Right;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public ActorBaseStats Stats { get; }
    public bool IsAlive => Stats.Health.Value > 0;
    public bool IsLeashed = true;
    public IHitbox Hitbox
    {
        get => new RectangleHitbox((int)Position.X - 8, (int)Position.Y - 16, 16, 32);
    }
    public int Level { get; }
    public int XP { get; } = 10;

    private SkeletonGraphicsComponent graphicsComponent;
    private SkeletonBehaviorComponent _behaviorComponent = new();

    public Skeleton(int level)
    {
        Level = level;
        Stats = new(this, speed: 150, health: 40);

        graphicsComponent = new(this);
    }

    public void Update(GameTime gameTime)
    {
        Stats.Update(gameTime);

        if (!IsAlive)
            State.Value = ActorState.Dead;

        graphicsComponent.Position = Position;
        graphicsComponent.Update(gameTime);
        _behaviorComponent.Update(this, gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        graphicsComponent.Draw(spriteBatch);
    }

    public void TakeDamage(double amount)
    {
        if (Stats.Health.Value <= 0)
            return;

        Stats.OffsetHealth(-amount);
        if (Stats.Health.Value <= 0)
        {
            Game1.World.Player.OnKill(this);
        }

        IsLeashed = true;
    }
}
