using Microsoft.Xna.Framework;

public class HolyFireEntity : SkillEntity
{
    public override IHitbox Hitbox
        => new CircleHitbox(Position, Radius);

    public double Radius = 100d;
    public double Damage = 100d;
    public double SelfDamage = 2d;

    private CircleSprite sprite;

    public HolyFireEntity(Entity parent) : base(parent)
    {
        AddDrawable(sprite = new CircleSprite((int)Radius)
        {
            Color = new Color(205, 45, 10, 64),
        });
    }

    protected override SkillBehaviorComponent CreateBehavior()
        => new HolyFireBehaviorComponent(this);
}
