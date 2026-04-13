using Microsoft.Xna.Framework;

public abstract class SkillEntity : Entity
{
    public Entity Parent { get; }

    private SkillBehaviorComponent behaviorComponent;

    public SkillEntity(Entity parent)
    {
        Parent = parent;
        behaviorComponent = CreateBehavior();
        Game1.World.AddEntity(this);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        behaviorComponent.Update(gameTime);
    }

    protected abstract SkillBehaviorComponent CreateBehavior();


    public override void Destroy()
    {
        base.Destroy();
        behaviorComponent.Destroy();
    }
}