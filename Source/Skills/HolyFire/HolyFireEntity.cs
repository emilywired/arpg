using Microsoft.Xna.Framework;

public class HolyFireEntity : SkillEntity
{
    public override IHitbox Hitbox
        => new CircleHitbox(Position, Radius);

    public double Radius = 100d;
    public double Damage = 50d;
    public double SelfDamage = 2d;

    public HolyFireEntity(Actor owner) : base(owner)
    {
        AddDrawable(new CircleSprite((int)Radius)
        {
            Color = new Color(205, 45, 10, 64),
        });
    }

    protected override SkillBehaviorComponent CreateBehavior()
        => new HolyFireBehaviorComponent(this);
}
