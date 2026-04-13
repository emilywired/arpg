public class FrozenOrbEntity : SkillEntity
{
    public float Speed { get; set; } = 100f;
    public readonly float MaxDuration = 3f;
    public double Angle = 0d;
    public float Rotation = 0f;

    private AnimatedSprite animatedSprite;

    public override IHitbox Hitbox
        => new RectangleHitbox(0, 0, 0, 0);

    public FrozenOrbEntity(Actor owner) : base(owner)
    {
        AddDrawable(animatedSprite = new AnimatedSprite(Assets.Spells.FrozenOrb));
    }

    public override void Update(float dt)
    {
        Rotation += dt * 3f;
        base.Update(dt);
        animatedSprite.Rotation = Rotation;
    }

    protected override SkillBehaviorComponent CreateBehavior()
        => new FrozenOrbBehaviorComponent(this);
}
