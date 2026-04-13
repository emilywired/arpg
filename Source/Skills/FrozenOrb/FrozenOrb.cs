public class FrozenOrb(Actor owner) : ISkill
{
    public string Name { get; } = "Frozen Orb";
    public Cooldown Cooldown { get; } = new(0.5f);
    private Actor owner = owner;

    public void Cast(double angle)
    {
        if (!Cooldown.CanCast())
        {
            return;
        }

        _ = new FrozenOrbEntity(owner)
        {
            Position = owner.Position,
            Angle = angle,
        };
        Cooldown.StartCooldown();
    }
}
