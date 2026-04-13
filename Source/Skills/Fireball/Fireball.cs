public class Fireball(Actor _owner) : ISkill
{
    public string Name { get; } = "Fireball";
    public Cooldown Cooldown { get; } = new(1.5f);
    private Actor owner = _owner;

    public void Cast(double angle)
    {
        if (!Cooldown.CanCast())
            return;

        _ = new FireballEntity(owner)
        {
            Position = new(owner.Position.X, owner.Position.Y),
            Angle = angle,
        };

        Cooldown.StartCooldown();
    }
}
