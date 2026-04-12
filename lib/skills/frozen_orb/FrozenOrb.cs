public class FrozenOrb(Actor owner) : ISkill
{
    public string Name { get; } = "Frozen Orb";
    public Cooldown Cooldown { get; } = new(0.5f);
    private Actor _owner = owner;

    public void Cast(double angle)
    {
        if (!Cooldown.CanCast())
        {
            return;
        }

        FrozenOrbEntity frozenOrbEntity = new(_owner)
        {
            Position = new(_owner.Position.X, _owner.Position.Y),
            Angle = angle,
        };
        Cooldown.StartCooldown();
    }
}
