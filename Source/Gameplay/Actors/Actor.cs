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
    public abstract ActorBaseStats Stats { get; }
    public bool IsAlive { get; private set; } = true;

    public virtual void TakeDamage(double amount)
    {
        Stats.OffsetHealth(-amount);
        if (Stats.Health.Value <= 0)
        {
            IsAlive = false;
        }
    }
}
