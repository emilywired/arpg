using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class FrozenOrbEntity(Entity parent) : SkillEntity(parent)
{
    public float Speed { get; set; } = 100f;
    public readonly float MaxDuration = 3f;
    public double Angle = 0d;
    public float Rotation = 0f;
    public override IHitbox Hitbox => new RectangleHitbox(0, 0, 0, 0);

    public override void Update(GameTime gameTime)
    {
        Rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * 3f;
        base.Update(gameTime);
    }

    protected override SkillBehaviorComponent CreateBehavior()
        => new FrozenOrbBehaviorComponent(this);

    protected override IEnumerable<DrawNode> CreateCompositeDrawNodes()
    {
        yield return new FrozenOrbDrawNode(this);
    }
}
