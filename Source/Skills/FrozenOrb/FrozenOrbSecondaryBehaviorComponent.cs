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
        float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        CurrentDuration += elapsedTime;

        if (CurrentDuration >= secondaryEntity.MaxDuration)
        {
            secondaryEntity.Destroy();
            return;
        }

        float x =
            secondaryEntity.Position.X
            + (secondaryEntity.Speed * elapsedTime * MathF.Cos(secondaryEntity.Angle));
        float y =
            secondaryEntity.Position.Y
            + (secondaryEntity.Speed * elapsedTime * MathF.Sin(secondaryEntity.Angle));
        secondaryEntity.Position = new(x, y);

        foreach (Monster actor in Game1.World.Entities.OfType<Monster>())
        {
            if (!_hitActors.Contains(actor.Id) && secondaryEntity.Hitbox.Intersects(actor.Hitbox))
            {
                actor.TakeDamage(secondaryEntity.Damage);
                _hitActors.Add(actor.Id);
            }
        }
    }
}
