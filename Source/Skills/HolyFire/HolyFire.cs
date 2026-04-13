public class HolyFire(Actor owner) : ISkill
{
    public string Name { get; } = "Holy Fire";
    public Cooldown Cooldown { get; } = new(1f);

    private Actor owner = owner;
    private HolyFireEntity? entity;
    private bool isActive = false;

    public void Cast(double angle)
    {
        if (!Cooldown.CanCast())
            return;

        if (!isActive)
        {
            HolyFireEntity holyFireEntity = new(owner)
            {
                Position = owner.Position,
                Radius = 100f,
            };

            entity = holyFireEntity;
            isActive = true;
        }
        else
        {
            entity!.Destroy();
            isActive = false;
        }

        Cooldown.StartCooldown();
    }
}
