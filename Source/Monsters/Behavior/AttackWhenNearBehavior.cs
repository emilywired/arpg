public class AttackWhenNearBehavior(Monster monster) : Behavior(monster)
{
    private bool isAttacking = false;
    private bool playerWasHit = false;
    private float attackInterval = 0.9f;
    private double swingTimer = 0.3f;
    private const float ATTACK_LAND_FRAME = 0.6f;

    public override void Update(float dt)
    {
        float distance = monster.Position.DistanceTo(Game1.World.Player.Position);

        bool withinAttackHitDistance = distance <= 64;
        bool withinAttackTriggerDistance = distance <= 32;

        if (withinAttackTriggerDistance && !isAttacking)
        {
            monster.ActionState.Value = ActorActionState.Swinging;
            monster.CanMove = false;
            isAttacking = true;
        }

        if (isAttacking)
        {
            swingTimer += dt;

            if (swingTimer >= ATTACK_LAND_FRAME)
            {
                // TODO: there should be an attack duration i.e. how long can you get hit for after starting swinging
                if (withinAttackHitDistance && !playerWasHit)
                {
                    // TODO: proper hitbox checking
                    Game1.World.Player.TakeDamage(monster.BaseDamage);
                    playerWasHit = true;
                }
            }

            if (swingTimer >= attackInterval)
            {
                swingTimer = 0f;
                isAttacking = false;
                playerWasHit = false;

                monster.ActionState.Value = ActorActionState.None;
                monster.CanMove = true;
            }
        }
    }
}
