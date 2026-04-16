using Microsoft.Xna.Framework;

public class FireballEntity : SkillEntity
{
    public float Speed { get; set; } = 300f;
    public double Angle = 0d;
    public DamagePacket BaseDamage = new(fire: 10);
    public readonly float MaxDuration = 2f;

    private RectangleHitbox localHitbox = new(-8, -8, 16, 16);
    public override IHitbox Hitbox =>
        localHitbox with
        {
            Bounds = localHitbox.Bounds with
            {
                X = (int)Position.X + localHitbox.Bounds.X,
                Y = (int)Position.Y + localHitbox.Bounds.Y,
            },
        };

    private AnimatedSprite animatedSprite;
    private RectangleSprite debug;

    public FireballEntity(Actor owner)
        : base(owner)
    {
        AddDrawable(animatedSprite = new AnimatedSprite(Assets.Spells.Fireball));
        AddDrawable(
            debug = new RectangleSprite
            {
                Position = localHitbox.Bounds.Location.ToVector2(),
                Size = localHitbox.Bounds.Size.ToVector2(),
                Color = Color.Yellow,
            }
        );
    }

    public override void Update(float dt)
    {
        base.Update(dt);
        animatedSprite.Rotation = (float)Angle;
        debug.Hidden = !GameState.IsDebugMode;
    }

    protected override SkillBehaviorComponent CreateBehavior() =>
        new FireballBehaviorComponent(this);
}
