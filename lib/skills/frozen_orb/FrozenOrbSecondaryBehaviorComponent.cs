using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

public class FrozenOrbSecondaryBehaviorComponent
{
    public float CurrentDuration = 0f;
    private List<string> _hitActors = [];

    public void Update(FrozenOrbSecondaryEntity secondaryEntity, GameTime gameTime)
    {
        var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        CurrentDuration += elapsedTime;

        if (CurrentDuration >= secondaryEntity.MaxDuration)
        {
            secondaryEntity.Destroy();
            return;
        }

        double x =
            secondaryEntity.Position.X
            + (secondaryEntity.Speed * elapsedTime * Math.Cos(secondaryEntity.Angle));
        double y =
            secondaryEntity.Position.Y
            + (secondaryEntity.Speed * elapsedTime * Math.Sin(secondaryEntity.Angle));
        secondaryEntity.Position = new((float)x, (float)y);

        foreach (IActor actor in Game1.World.Actors.Where(actor => actor.Kind == ActorKind.Monster))
        {
            if (!_hitActors.Contains(actor.Id) && secondaryEntity.Hitbox.Intersects(actor.Hitbox))
            {
                actor.TakeDamage(secondaryEntity.Damage);
                _hitActors.Add(actor.Id);
            }
        }
    }
}
