using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HolyFireEntity(Entity parent) : SkillEntity(parent)
{
    public override IHitbox Hitbox
        => new CircleHitbox(Position, Radius);

    public double Radius = 100d;
    public double Damage = 100d;
    public double SelfDamage = 2d;

    public Texture2D? Texture { get; private set; }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Texture = Assets.CreateCircleTexture((int)Radius);
    }

    protected override SkillBehaviorComponent CreateBehavior()
        => new HolyFireBehaviorComponent(this);

    protected override IEnumerable<DrawNode> CreateCompositeDrawNodes()
    {
        yield return new HolyFireDrawNode(this);
    }
}
