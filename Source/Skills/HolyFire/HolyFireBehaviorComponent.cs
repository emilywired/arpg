using System.Collections.Generic;
using System.Linq;

public class HolyFireBehaviorComponent : SkillBehaviorComponent<HolyFireEntity>
{
    private List<Actor> intersectingEntities = [];

    public HolyFireBehaviorComponent(HolyFireEntity parent)
        : base(parent)
    {
        parent.Owner.Stats.AddHealthDegen(parent.SelfDamage);
    }

    public override void Update(float dt)
    {
        // TODO: replace 20 with the half width of the visible player sprite
        Parent.Position = new(Parent.Owner.Position.X + 0, Parent.Owner.Position.Y + 20);

        foreach (
            Actor? actor in Game1
                .World.Entities.OfType<Actor>()
                .Where(actor => actor != Parent.Parent)
        )
        {
            bool wasAlreadyIntersecting = intersectingEntities.Contains(actor);
            bool intersects = actor.Hitbox.Intersects(Parent.Hitbox);

            if (!wasAlreadyIntersecting && intersects)
            {
                actor.Stats.AddHealthDegen(Parent.Damage);
                intersectingEntities.Add(actor);
            }
            else if (wasAlreadyIntersecting && !intersects)
            {
                actor.Stats.SubtractHealthDegen(Parent.Damage);
                _ = intersectingEntities.Remove(actor);
            }
        }
    }

    public override void Destroy()
    {
        Parent.Owner.Stats.SubtractHealthDegen(Parent.SelfDamage);

        foreach (
            Actor? actor in Game1
                .World.Entities.OfType<Actor>()
                .Where(actor => actor != Parent.Owner)
        )
        {
            bool wasAlreadyIntersecting = intersectingEntities.Contains(actor);
            if (wasAlreadyIntersecting)
            {
                actor.Stats.SubtractHealthDegen(Parent.Damage);
            }
        }
    }
}
