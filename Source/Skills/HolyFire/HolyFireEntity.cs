using Microsoft.Xna.Framework;

public class HolyFireEntity : SkillEntity
{
    public override IHitbox Hitbox => new CircleHitbox(Position, Radius);

    public double Radius = 100d;

    // TODO: don't do this
    public DamagePacket Damage = new(fire: 50);
    public DamagePacket SelfDamage = new(fire: 5);

    /*
    DamagePacket
    Scale(-1)
    AddHealthRate()
    ApplyTick()
    ApplyResistances
    OffsetHealth(final * dt)
    */

    public HolyFireEntity(Actor owner)
        : base(owner)
    {
        AddDrawable(new CircleSprite((int)Radius) { Color = new Color(205, 45, 10, 64) });
    }

    protected override SkillBehaviorComponent CreateBehavior() =>
        new HolyFireBehaviorComponent(this);
}
