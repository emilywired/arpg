using Microsoft.Xna.Framework;

public class MovementFollow(Monster monster, Actor target) : MovementBehavior(monster)
{
    public override void Update(GameTime gameTime)
    {
        var dt = gameTime.ElapsedGameTime.TotalSeconds;
        base.Update(gameTime);

        var angle = monster.Position.AngleTo(target.Position);
        var vector = Vector2.Rotate(Vector2.UnitX, angle);
        DesiredVelocity = (float)monster.Stats.Speed * (float)dt * vector;

        monster.Facing = DesiredVelocity.X < 0 ? ActorFacing.Left : ActorFacing.Right;
    }
}