using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Monster : IActor
{
    public string Id { get; } = Guid.NewGuid().ToString();

    public ActorKind Kind { get; } = ActorKind.Monster;

    public ReactiveProperty<ActorState> State { get; } = new(ActorState.Idling);
    ActorState IActor.State => State.Value;

    public ReactiveProperty<ActorActionState> ActionState = new(ActorActionState.None);
    ActorActionState IActor.ActionState => ActionState.Value;

    public ActorFacing Facing { get; set; } = ActorFacing.Right;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public ActorBaseStats Stats { get; protected set; }
    public bool IsAlive => Stats.Health.Value > 0;
    public IHitbox Hitbox => new RectangleHitbox((int)Position.X - 8, (int)Position.Y - 16, 16, 32);

    public int XP { get; }
    public int Level { get; }
    public bool IsLeashed { get; set; } = true;
    public bool CanMove { get; set; } = true;

    protected MovementBehavior movementBehavior;
    protected List<Behavior> behaviors = [];

    private float corpseDespawnTime = 10f;
    private float timeSinceDeath = 0;

    public Monster(int level)
    {
        Level = level;
        Stats = new(this, speed: 150, health: 40);

        movementBehavior = new(this);
    }

    public virtual void Update(GameTime gameTime)
    {
        var dt = gameTime.ElapsedGameTime.TotalSeconds;
        Stats.Update(gameTime);

        if (!IsAlive)
            State.Value = ActorState.Dead;

        if (State.Value == ActorState.Dead)
        {
            timeSinceDeath += (float)dt;
            if (timeSinceDeath >= corpseDespawnTime)
                Game1.World.RemoveActor(this);

            ActionState.Value = ActorActionState.None;
            return;
        }

        foreach (var behavior in behaviors)
            behavior.Update(gameTime);

        if (IsAlive)
        {
            movementBehavior.Update(gameTime);
            if (CanMove && movementBehavior.DesiredVelocity != Vector2.Zero)
            {
                State.Value = ActorState.Walking;
                Position += movementBehavior.DesiredVelocity;
            } else
            {
                State.Value = ActorState.Idling;
            }
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
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