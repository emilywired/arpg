public interface ISkill
{
    string Name { get; }
    Cooldown Cooldown { get; }
    void Cast(double angle);
}
