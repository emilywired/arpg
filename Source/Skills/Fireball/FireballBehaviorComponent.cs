using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

public class FireballBehaviorComponent(FireballEntity parent) : SkillBehaviorComponent<FireballEntity>(parent)
{
    private List<Actor> hitActors = [];

    public override void Update(GameTime gameTime)
    {
        float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        CurrentDuration += elapsedTime;

        if (CurrentDuration >= Parent.MaxDuration)
        {
            Parent.Destroy();
            return;
        }

        double x = Parent.Position.X + (Parent.Speed * elapsedTime * Math.Cos(Parent.Angle));
        double y = Parent.Position.Y + (Parent.Speed * elapsedTime * Math.Sin(Parent.Angle));
        Parent.Position = new((float)x, (float)y);

        foreach (
            Actor? actor in Game1
                .World.Entities.OfType<Actor>()
                .Where(actor => actor != Parent.Parent)
        )
        {
            if (!hitActors.Contains(actor) && Parent.Hitbox.Intersects(actor.Hitbox))
            {
                actor.TakeDamage(Parent.Damage);
                hitActors.Add(actor);
            }
        }
    }
}
