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

    private ProgressBar healthBar;
    private float corpseDespawnTime = 10f;
    private float timeSinceDeath = 0;

    public Monster(int level)
    {
        Level = level;
        Stats = new(this, speed: 400, health: 40);

        healthBar = new ProgressBar()
        {
            MaxValue = Stats.MaxHealth.Value,
            Size = new(32, 4),
            Color = Colors.Health,
            CenterHorizontally = true,
            Position = Position,
        };

        Stats.Health.Connect(this, value => healthBar.Value = value);
        Stats.MaxHealth.Connect(this, value => healthBar.MaxValue = value);

        movementBehavior = new(this);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        double dt = gameTime.ElapsedGameTime.TotalSeconds;
        Stats.Update(gameTime);

        if (!IsAlive)
            State.Value = ActorState.Dead;

        if (State.Value == ActorState.Dead)
        {
            timeSinceDeath += (float)dt;
            if (timeSinceDeath >= corpseDespawnTime)
                Game1.World.RemoveEntity(this);

            ActionState.Value = ActorActionState.None;
            return;
        }

        foreach (Behavior behavior in behaviors)
            behavior.Update(gameTime);

        if (IsAlive)
        {
            movementBehavior.Update(gameTime);
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

        healthBar.Position = Position - new Vector2(0, 20);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        if (Game1.Config.DisplayEnemyHealthBars && IsAlive)
            healthBar.Draw(spriteBatch);
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
