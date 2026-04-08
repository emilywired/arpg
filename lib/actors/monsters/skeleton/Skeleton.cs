using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Skeleton : IMonster
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public ActorKind Kind { get; } = ActorKind.Monster;

    public ActorState State { 
        get => field; 
        set
        {
            var changed = field != value;
            field = value;
            if (changed)
                OnStateChanged?.Invoke();
        }
    } = ActorState.Idling;
    public event Action? OnStateChanged;

    public ActorActionState ActionState { 
        get => field; 
        set
        {
            var changed = field != value;
            field = value;
            if (changed)
                OnActionStateChanged?.Invoke();
        }
    } = ActorActionState.None;
    public event Action? OnActionStateChanged;

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

    private SkeletonSprite sprite;
    private SkeletonBehaviorComponent _behaviorComponent = new();

    public Skeleton(int level)
    {
        Level = level;
        Stats = new(this, speed: 150, health: 40);

        sprite = new(this);
    }

    public void Update(GameTime gameTime)
    {
        Stats.Update(gameTime);

        if (!IsAlive)
            State = ActorState.Dead;

        sprite.Position = Position;
        sprite.Update(gameTime);
        _behaviorComponent.Update(this, gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch);
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
}
