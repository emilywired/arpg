public abstract class SkillBehaviorComponent(SkillEntity parent)
{
    public SkillEntity Parent { get; } = parent;

    public float CurrentDuration = 0f;

    public virtual void Update(float dt)
    {
        CurrentDuration += dt;
    }

    public virtual void Destroy() { }
}

public abstract class SkillBehaviorComponent<T>(T parent) : SkillBehaviorComponent(parent)
    where T : SkillEntity
{
    public new T Parent => (T)base.Parent;
}