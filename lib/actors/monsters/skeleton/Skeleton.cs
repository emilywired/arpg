using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Skeleton : Monster
{
    private SkeletonGraphicsComponent graphicsComponent;

    public Skeleton(int level)
        : base(level)
    {
        Stats = new(this, speed: 150, health: 40);

        graphicsComponent = new(this);
        movementBehavior = new MovementFollow(this, Game1.World.Player);
        behaviors.Add(new AttackWhenNearBehavior(this));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        graphicsComponent.Position = Position;
        graphicsComponent.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        graphicsComponent.Draw(spriteBatch);
    }
}
