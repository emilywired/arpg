public class Fireball(Actor owner) : ISkill
{
    public string Name { get; } = "Fireball";
    public Cooldown Cooldown { get; } = new(1.5f);
    private Actor owner = owner;

    public void Cast(double angle)
    {
        if (!Cooldown.CanCast())
            return;

        _ = new FireballEntity(owner)
        {
            Position = owner.Position,
            Angle = angle,
        };

        Cooldown.StartCooldown();
    }
}
