using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

public class HolyFireBehaviorComponent
{
    private List<Actor> intersectingEntities = [];

    public HolyFireBehaviorComponent(HolyFireEntity holyFire)
    {
        holyFire.Owner.Stats.AddHealthDegen(holyFire.SelfDamage);
    }

#pragma warning disable IDE0060
    public void Update(HolyFireEntity holyFire, GameTime gameTime)
#pragma warning restore IDE0060
    {
        // TODO: replace 20 with the half width of the visible player sprite
        holyFire.Position = new(holyFire.Owner.Position.X + 0, holyFire.Owner.Position.Y + 20);

        foreach (
            Actor? actor in Game1
                .World.Entities.OfType<Actor>()
                .Where(actor => actor != holyFire.Owner)
        )
        {
            bool wasAlreadyIntersecting = intersectingEntities.Contains(actor);
            bool intersects = actor.Hitbox.Intersects(holyFire.Hitbox);

            if (!wasAlreadyIntersecting && intersects)
            {
                actor.Stats.AddHealthDegen(holyFire.Damage);
                intersectingEntities.Add(actor);
            }
            else if (wasAlreadyIntersecting && !intersects)
            {
                actor.Stats.SubtractHealthDegen(holyFire.Damage);
                _ = intersectingEntities.Remove(actor);
            }
        }
    }

    public void Destroy(HolyFireEntity holyFire)
    {
        holyFire.Owner.Stats.SubtractHealthDegen(holyFire.SelfDamage);

        foreach (
            Actor? actor in Game1
                .World.Entities.OfType<Actor>()
                .Where(actor => actor != holyFire.Owner)
        )
        {
            bool wasAlreadyIntersecting = intersectingEntities.Contains(actor);
            if (wasAlreadyIntersecting)
            {
                actor.Stats.SubtractHealthDegen(holyFire.Damage);
            }
        }
    }
}
