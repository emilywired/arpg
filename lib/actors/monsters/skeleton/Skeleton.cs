using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Skeleton : IMonster
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public ActorKind Kind { get; } = ActorKind.Monster;
    public ActorState State { get; set; } = ActorState.Idling;
    public ActorActionState ActionState { get; set; } = ActorActionState.None;
    public ActorFacing Facing { get; set; } = ActorFacing.Right;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public ActorBaseStats Stats { get; }
    public bool IsAlive => Stats.Health > 0;
    public bool IsLeashed = true;
    public IHitbox Hitbox
    {
        get => new RectangleHitbox((int)Position.X - 8, (int)Position.Y - 16, 16, 32);
    }
    public int Level { get; }
    public int XP { get; } = 10;

    private SkeletonGraphicsComponent _graphicsComponent = new();
    private SkeletonBehaviorComponent _behaviorComponent = new();

    public Skeleton(int level)
    {
        Level = level;
        Stats = new(this, speed: 150, health: 40);
    }

    public void Update(GameTime gameTime)
    {
        Stats.Update(gameTime);

        if (!IsAlive)
        {
            TransitionState(ActorState.Dead);
        }

        _graphicsComponent.Update(this, gameTime);
        _behaviorComponent.Update(this, gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _graphicsComponent.Draw(this, spriteBatch);
    }

    public void TakeDamage(double amount)
    {
        if (Stats.Health <= 0)
            return;

        Stats.OffsetHealth(-amount);
        if (Stats.Health <= 0)
        {
            Game1.World.Player.OnKill(this);
        }

        IsLeashed = true;
    }

    public void TransitionState(ActorState newState)
    {
        bool stateChanged = State != newState;
        if (stateChanged)
        {
            State = newState;
            _graphicsComponent.ResetFrames();
        }
    }

    public void TransitionState(ActorActionState newState)
    {
        bool stateChanged = ActionState != newState;
        if (stateChanged)
        {
            ActionState = newState;
            _graphicsComponent.ResetFrames();
        }
    }
}
