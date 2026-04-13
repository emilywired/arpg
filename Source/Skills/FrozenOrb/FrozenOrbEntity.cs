using Microsoft.Xna.Framework;

public class FrozenOrbEntity : SkillEntity
{
    public float Speed { get; set; } = 100f;
    public readonly float MaxDuration = 3f;
    public double Angle = 0d;
    public float Rotation = 0f;

    private AnimatedSprite animatedSprite;

    public override IHitbox Hitbox
        => new RectangleHitbox(0, 0, 0, 0);

    public FrozenOrbEntity(Entity parent) : base(parent)
    {
        AddDrawable(animatedSprite = new AnimatedSprite(Assets.Spells.FrozenOrb));
    }

    public override void Update(GameTime gameTime)
    {
        Rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * 3f;
        base.Update(gameTime);
        animatedSprite.Update(gameTime);
    }

    protected override SkillBehaviorComponent CreateBehavior()
        => new FrozenOrbBehaviorComponent(this);
}
