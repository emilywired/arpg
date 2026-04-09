using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

public class HolyFireBehaviorComponent
{
    private List<IActor> _intersectingEntities = [];

    public HolyFireBehaviorComponent(HolyFireEntity holyFire)
    {
        holyFire.Owner.Stats.AddHealthDegen(holyFire.SelfDamage);
    }

    public void Update(HolyFireEntity holyFire, GameTime gameTime)
    {
        // TODO: replace 20 with the half width of the visible player sprite
        holyFire.Position = new(holyFire.Owner.Position.X + 0, holyFire.Owner.Position.Y + 20);

        foreach (var actor in Game1.World.Actors.Where(actor => actor.Kind != holyFire.Owner.Kind))
        {
            bool wasAlreadyIntersecting = _intersectingEntities.Contains(actor);
            bool intersects = actor.Hitbox.Intersects(holyFire.Hitbox);

            if (!wasAlreadyIntersecting && intersects)
            {
                actor.Stats.AddHealthDegen(holyFire.Damage);
                _intersectingEntities.Add(actor);
            }
            else if (wasAlreadyIntersecting && !intersects)
            {
                actor.Stats.SubtractHealthDegen(holyFire.Damage);
                _intersectingEntities.Remove(actor);
            }
        }
    }

    public void Destroy(HolyFireEntity holyFire)
    {
        holyFire.Owner.Stats.SubtractHealthDegen(holyFire.SelfDamage);

        foreach (var actor in Game1.World.Actors.Where(actor => actor.Kind != holyFire.Owner.Kind))
        {
            bool wasAlreadyIntersecting = _intersectingEntities.Contains(actor);
            if (wasAlreadyIntersecting)
            {
                actor.Stats.SubtractHealthDegen(holyFire.Damage);
            }
        }
    }
}
