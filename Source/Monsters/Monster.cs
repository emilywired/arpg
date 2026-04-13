using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Monster : Actor
{
    public override ActorBaseStats Stats { get; }
    public override IHitbox Hitbox =>
        new RectangleHitbox((int)Position.X - 8, (int)Position.Y - 16, 16, 32);

    public int XP { get; }
    public int Level { get; }
    public bool IsLeashed { get; set; } = true;
    public bool CanMove { get; set; } = true;

    protected MovementBehavior movementBehavior;
    protected List<Behavior> behaviors = [];
    protected AnimatedSprite sprite;

    private ProgressBar healthBar;
    private float corpseDespawnTime = 10f;
    private float timeSinceDeath = 0;


    public Monster(int level)
    {
        Level = level;
        Stats = new(this, speed: 400, health: 40);

        AddDrawable(healthBar = new ProgressBar()
        {
            MaxValue = Stats.MaxHealth.Value,
            Size = new(32, 4),
            Color = Colors.Health,
            CenterHorizontally = true,
            Position = new Vector2(0, -20),
        });

        Stats.Health.Connect(this, value => healthBar.Value = value);
        Stats.MaxHealth.Connect(this, value => healthBar.MaxValue = value);

        movementBehavior = new(this);
        AddDrawable(sprite = new());
    }

    public override void Update(float dt)
    {
        base.Update(dt);

        Stats.Update(dt);

        sprite.SpriteEffects = Facing == ActorFacing.Right
            ? SpriteEffects.None
            : SpriteEffects.FlipHorizontally;
        sprite.Hidden = !Game1.Config.DisplayEnemyHealthBars || !IsAlive;

        if (!IsAlive)
            State.Value = ActorState.Dead;

        if (State.Value == ActorState.Dead)
        {
            timeSinceDeath += dt;
            if (timeSinceDeath >= corpseDespawnTime)
                Game1.World.RemoveEntity(this);

            ActionState.Value = ActorActionState.None;
            return;
        }

        foreach (Behavior behavior in behaviors)
            behavior.Update(dt);

        if (IsAlive)
        {
            movementBehavior.Update(dt);
            if (CanMove && movementBehavior.DesiredVelocity != Vector2.Zero)
            {
                State.Value = ActorState.Walking;
                Position += movementBehavior.DesiredVelocity;
            }
            else
            {
                State.Value = ActorState.Idling;
            }
        }
    }

    public override void TakeDamage(double amount)
    {
        if (!IsAlive)
            return;

        base.TakeDamage(amount);

        if (Stats.Health.Value <= 0)
        {
            Game1.World.Player.OnKill(this);
        }

        IsLeashed = true;
    }
}
