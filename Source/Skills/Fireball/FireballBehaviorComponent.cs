using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

public class FireballBehaviorComponent
{
    private List<Actor> hitActors = [];
    public float CurrentDuration = 0f;

    public void Update(FireballEntity fireball, GameTime gameTime)
    {
        float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        CurrentDuration += elapsedTime;

        if (CurrentDuration >= fireball.MaxDuration)
        {
            fireball.Destroy();
            return;
        }

        double x = fireball.Position.X + (fireball.Speed * elapsedTime * Math.Cos(fireball.Angle));
        double y = fireball.Position.Y + (fireball.Speed * elapsedTime * Math.Sin(fireball.Angle));
        fireball.Position = new((float)x, (float)y);

        foreach (
            Actor? actor in Game1
                .World.Entities.OfType<Actor>()
                .Where(actor => actor != fireball.Owner)
        )
        {
            if (!hitActors.Contains(actor) && fireball.Hitbox.Intersects(actor.Hitbox))
            {
                actor.TakeDamage(fireball.Damage);
                hitActors.Add(actor);
            }
        }
    }
}
