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

    public virtual void TakeDamage(DamagePacket damage)
    {
        bool wasAlreadyDead = !IsAlive;

        DamagePacket damageAfterResistances = DamageCalculations.WithResistances(damage, Stats);
        Stats.OffsetHealth(-damageAfterResistances.Sum());

        if (!wasAlreadyDead && !IsAlive)
        {
            OnDeath();
        }
    }

    public abstract void OnDeath();

    private void ApplyTick(float dt)
    {
        foreach (DamagePacket damageSource in Stats.HealthRateSources.Values)
        {
            DamagePacket damageAfterResistances = DamageCalculations.WithResistances(
                damageSource,
                Stats
            );

            DamagePacket damageDelta = damageAfterResistances.Scale(-dt);
            TakeDamage(damageDelta);
        }

        double manaRateDelta = Stats.GetManaRateDelta(dt);
        Stats.OffsetMana(manaRateDelta);
    }
}
