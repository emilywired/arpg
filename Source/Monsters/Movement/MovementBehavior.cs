using Microsoft.Xna.Framework;

public class MovementBehavior(Monster monster) : IUpdateable
{
    public Vector2 DesiredVelocity { get; protected set; }

    protected Monster monster = monster;

    public virtual void Update(float dt)
    {
    }
}