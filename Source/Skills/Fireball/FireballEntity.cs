using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class FireballEntity(Entity parent) : SkillEntity(parent)
{
    public float Speed { get; set; } = 300f;
    public double Angle = 0d;
    public float Damage = 10f;
    public readonly float MaxDuration = 2f;
    public override IHitbox Hitbox
        => new RectangleHitbox((int)Position.X - 8, (int)Position.Y - 8, 16, 16);

    public TextureAsset Asset { get; } = Assets.Spells.Fireball;
    public int CurrentFrame { get; private set; }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        CurrentFrame++;
        CurrentFrame %= Asset.Frames.Count;
    }

    protected override SkillBehaviorComponent CreateBehavior()
        => new FireballBehaviorComponent(this);

    protected override IEnumerable<DrawNode> CreateCompositeDrawNodes()
    {
        yield return new FireballDrawNode(this);
    }
}
