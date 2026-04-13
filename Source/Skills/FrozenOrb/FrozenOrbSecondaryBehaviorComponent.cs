using System;
using System.Collections.Generic;
using System.Linq;

public class FrozenOrbSecondaryBehaviorComponent(FrozenOrbSecondaryEntity parent)
    : SkillBehaviorComponent<FrozenOrbSecondaryEntity>(parent)
{
    private List<string> hitActors = [];

    public override void Update(float dt)
    {
        base.Update(dt);
        if (CurrentDuration >= Parent.MaxDuration)
        {
            Parent.Destroy();
            return;
        }

        float x =
            Parent.Position.X
            + (Parent.Speed * dt * MathF.Cos(Parent.Angle));
        float y =
            Parent.Position.Y
            + (Parent.Speed * dt * MathF.Sin(Parent.Angle));
        Parent.Position = new(x, y);

        foreach (Monster actor in Game1.World.Entities.OfType<Monster>())
        {
            if (!hitActors.Contains(actor.Id) && Parent.Hitbox.Intersects(actor.Hitbox))
            {
                actor.TakeDamage(Parent.Damage);
                hitActors.Add(actor.Id);
            }
        }
    }
}
