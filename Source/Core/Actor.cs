using System.Linq;

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
        DamageCalculations.ApplyResistances(damage, Stats);
        Stats.OffsetHealth(-damage.Types.Values.Sum());
    }

    private void ApplyTick(float dt)
    {
        double healthRate = 0;

        var damageTypes = Stats.HealthRateSources.Values.ToList();

        foreach (DamagePacket damage in damageTypes)
        {
            DamageCalculations.ApplyResistances(damage, Stats);
            healthRate += damage.Sum();
        }

        double healthRateDelta = healthRate * dt;
        Stats.OffsetHealth(healthRateDelta);

        double manaRateDelta = Stats.GetManaRateDelta(dt);
        Stats.OffsetMana(manaRateDelta);
    }
}
