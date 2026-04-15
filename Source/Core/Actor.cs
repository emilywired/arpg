public enum ActorState
{
    Idling,
    Walking,
    Dead,
}

public enum ActorActionState
{
    None,
    Swinging,
    Casting,
}

public enum ActorFacing
{
    Left,
    Right,
}

public abstract class Actor : Entity
{
    public ReactiveProperty<ActorState> State { get; } = new(ActorState.Idling);
    public ReactiveProperty<ActorActionState> ActionState { get; } = new(ActorActionState.None);
    public ActorFacing Facing { get; set; }
    public abstract ActorStats Stats { get; }
    public bool IsAlive => Stats.Health.Value > 0;

    public override void Update(float dt)
    {
        base.Update(dt);
        ApplyTick(dt);
    }

    public virtual void TakeDamage(double amount)
    {
        // TODO: damage calculations
        Stats.OffsetHealth(-amount);
    }

    private void ApplyTick(float dt)
    {
        double healthDelta = Stats.GetHealthDelta(dt);
        double manaDelta = Stats.GetManaDelta(dt);
        Stats.OffsetHealth(healthDelta);
        Stats.OffsetMana(manaDelta);
    }
}
