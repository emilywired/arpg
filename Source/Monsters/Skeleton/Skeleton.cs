using System;

public class Skeleton : Monster
{
    private TextureAsset idleAsset = Assets.Monsters.Skeleton.Idle;
    private TextureAsset attackAsset = Assets.Monsters.Skeleton.Attack;
    private TextureAsset walkAsset = Assets.Monsters.Skeleton.Walk;
    private TextureAsset deathAsset = Assets.Monsters.Skeleton.Death; // TODO: add one of the two corpse frames

    public Skeleton(int level)
        : base(level)
    {
        BaseDamage = new(physical: 10);
        Stats.MaxHealth.Value = 50;
        Stats.Health.Value = 50;
        Stats.Speed = 100;
        Stats.Resistances.Values[DamageType.Fire] = -75;

        movementBehavior = new MovementFollow(this, Game1.World.Player);
        behaviors.Add(new AttackWhenNearBehavior(this));

        State.Connect(this, onStateChanged);
        ActionState.Connect(this, onStateChanged);
    }

    private void onStateChanged()
    {
        sprite.SetTextureAsset(
            (State.Value, ActionState.Value) switch
            {
                // (ActorState.Walking, ActorActionState.Swinging) => _walkAttackAsset,
                (ActorState.Dead, _) => deathAsset,
                (_, ActorActionState.Swinging) => attackAsset,
                (ActorState.Idling, _) => idleAsset,
                (ActorState.Walking, _) => walkAsset,
                _ => throw new Exception("Unhandled ActorState"),
            }
        );
    }
}
