
public abstract class SkillEntity : Entity
{
    public Actor Owner { get; }

    private SkillBehaviorComponent behaviorComponent;

    public SkillEntity(Actor owner)
    {
        Owner = owner;
        behaviorComponent = CreateBehavior();
        Game1.World.AddEntity(this);
    }

    public override void Update(float dt)
    {
        base.Update(dt);
        behaviorComponent.Update(dt);
    }

    protected abstract SkillBehaviorComponent CreateBehavior();


    public override void Destroy()
    {
        base.Destroy();
        behaviorComponent.Destroy();
    }
}