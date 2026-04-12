public class Fireball(Actor owner) : ISkill
{
    public string Name { get; } = "Fireball";
    public Cooldown Cooldown { get; } = new(1.5f);
    private Actor _owner = owner;

    public void Cast(double angle)
    {
        if (!Cooldown.CanCast())
        {
            return;
        }

        FireballEntity fireballEntity = new(_owner)
        {
            Position = new(_owner.Position.X, _owner.Position.Y),
            Angle = angle,
        };

        Cooldown.StartCooldown();
    }
}
