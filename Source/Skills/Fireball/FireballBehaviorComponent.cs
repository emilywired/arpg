using System;
using System.Collections.Generic;
using System.Linq;

public class FireballBehaviorComponent(FireballEntity parent)
    : SkillBehaviorComponent<FireballEntity>(parent)
{
    private List<Actor> hitActors = [];

    public override void Update(float dt)
    {
        CurrentDuration += dt;

        if (CurrentDuration >= Parent.MaxDuration)
        {
            Parent.Destroy();
            return;
        }

        double x = Parent.Position.X + (Parent.Speed * dt * Math.Cos(Parent.Angle));
        double y = Parent.Position.Y + (Parent.Speed * dt * Math.Sin(Parent.Angle));
        Parent.Position = new((float)x, (float)y);

        foreach (
            Actor? actor in Game1
                .World.Entities.OfType<Actor>()
                .Where(actor => actor != Parent.Owner)
        )
        {
            if (!hitActors.Contains(actor) && Parent.Hitbox.Intersects(actor.Hitbox))
            {
                actor.TakeDamage(Parent.BaseDamage);
                hitActors.Add(actor);
            }
        }
    }
}
