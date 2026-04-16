using Microsoft.Xna.Framework;

public class FrozenOrbSecondaryEntity : SkillEntity
{
    public float Speed { get; set; } = 150f;
    public float Angle = 0f;
    public DamagePacket Damage = new(cold: 5);
    public readonly float MaxDuration = 1.5f;

    public override IHitbox Hitbox =>
        new RectangleHitbox((int)Position.X - 16, (int)Position.Y - 16, 32, 32);

    private AnimatedSprite animatedSprite;

    public FrozenOrbSecondaryEntity(Actor owner)
        : base(owner)
    {
        AddDrawable(animatedSprite = new AnimatedSprite(Assets.Spells.FrozenOrbSecondary));
    }

    public override void Update(float dt)
    {
        base.Update(dt);
        animatedSprite.Rotation = Angle + MathHelper.ToRadians(90);
    }

    protected override SkillBehaviorComponent CreateBehavior() =>
        new FrozenOrbSecondaryBehaviorComponent(this);
}
