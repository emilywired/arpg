using System.Collections.Generic;

public class FrozenOrbSecondaryEntity(Entity parent) : SkillEntity(parent)
{
    public float Speed { get; set; } = 150f;
    public float Angle = 0f;
    public float Damage = 5f;
    public readonly float MaxDuration = 1.5f;
    public override IHitbox Hitbox =>
        new RectangleHitbox((int)Position.X - 16, (int)Position.Y - 16, 32, 32);

    protected override SkillBehaviorComponent CreateBehavior()
        => new FrozenOrbSecondaryBehaviorComponent(this);

    protected override IEnumerable<DrawNode> CreateCompositeDrawNodes()
    {
        yield return new FrozenOrbSecondaryDrawNode(this);
    }
}
