using Microsoft.Xna.Framework;

public class MovementBehavior(Monster monster)
{
    public Vector2 DesiredVelocity { get; protected set; }

    protected Monster monster = monster;

    public virtual void Update(GameTime gameTime)
    {
    }
}