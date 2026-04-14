using System.Collections.Generic;
using System.Linq;

public class HolyFireBehaviorComponent : SkillBehaviorComponent<HolyFireEntity>
{
    private List<Actor> intersectingEntities = [];

    public HolyFireBehaviorComponent(HolyFireEntity parent)
        : base(parent)
    {
        parent.Owner.Stats.AddHealthRate(Parent, -Parent.SelfDamage);
    }

    public override void Update(float dt)
    {
        // TODO: replace 20 with the half width of the visible player sprite
        Parent.Position = new(Parent.Owner.Position.X + 0, Parent.Owner.Position.Y + 20);

        foreach (
            Actor? actor in Game1
                .World.Entities.OfType<Actor>()
                .Where(actor => actor != Parent.Owner)
        )
        {
            bool wasAlreadyIntersecting = intersectingEntities.Contains(actor);
            bool intersects = actor.Hitbox.Intersects(Parent.Hitbox);

            if (!wasAlreadyIntersecting && intersects)
            {
                actor.Stats.AddHealthRate(Parent, -Parent.Damage);
                intersectingEntities.Add(actor);
            }
            else if (wasAlreadyIntersecting && !intersects)
            {
                actor.Stats.RemoveHealthRate(Parent);
                _ = intersectingEntities.Remove(actor);
            }
        }
    }

    public override void Destroy()
    {
        Parent.Owner.Stats.RemoveHealthRate(Parent);

        foreach (
            Actor? actor in Game1
                .World.Entities.OfType<Actor>()
                .Where(actor => actor != Parent.Owner)
        )
        {
            bool wasAlreadyIntersecting = intersectingEntities.Contains(actor);
            if (wasAlreadyIntersecting)
            {
                actor.Stats.RemoveHealthRate(Parent);
            }
        }
    }
}
