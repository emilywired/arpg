public class FrozenOrb(Actor _owner) : ISkill
{
    public string Name { get; } = "Frozen Orb";
    public Cooldown Cooldown { get; } = new(0.5f);
    private Actor owner = _owner;

    public void Cast(double angle)
    {
        if (!Cooldown.CanCast())
        {
            return;
        }

        _ = new FrozenOrbEntity(owner)
        {
            Position = new(owner.Position.X, owner.Position.Y),
            Angle = angle,
        };
        Cooldown.StartCooldown();
    }
}
