public class SkillCollection(Actor owner)
{
    public Fireball Fireball = new(owner);
    public FrozenOrb FrozenOrb = new(owner);
    public HolyFire HolyFire = new(owner);
    private readonly Actor _owner = owner;
}
