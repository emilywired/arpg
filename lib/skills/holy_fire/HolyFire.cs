public class HolyFire(IActor owner) : ISkill
{
    public string Name { get; } = "Holy Fire";
    public Cooldown Cooldown { get; } = new(1f);
    private IActor _owner = owner;
    private bool isActive = false;
    private HolyFireEntity? _entity;

    public void Cast(double angle)
    {
        if (!Cooldown.CanCast())
            return;

        if (!isActive)
        {
            HolyFireEntity holyFireEntity = new(_owner)
            {
                Position = new(_owner.Position.X, _owner.Position.Y),
                Radius = 100f,
            };

            _entity = holyFireEntity;
            isActive = true;
        }
        else
        {
            _entity!.Destroy();
            isActive = false;
        }

        Cooldown.StartCooldown();
    }
}
