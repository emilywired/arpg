using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

public class FrozenOrbSecondaryBehaviorComponent(FrozenOrbSecondaryEntity parent)
    : SkillBehaviorComponent<FrozenOrbSecondaryEntity>(parent)
{
    private List<string> hitActors = [];

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (CurrentDuration >= Parent.MaxDuration)
        {
            Parent.Destroy();
            return;
        }

        float x =
            Parent.Position.X
            + (Parent.Speed * elapsedTime * MathF.Cos(Parent.Angle));
        float y =
            Parent.Position.Y
            + (Parent.Speed * elapsedTime * MathF.Sin(Parent.Angle));
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
