using System;
using arpg;
using Microsoft.Xna.Framework;

public class SkeletonBehaviorComponent
{
    private float _corpseDespawnTime = 10f;
    private bool _playerWasHit = false;
    private bool _isAttacking = false;
    private float _timeSinceDeath = 0f;
    private float _attackInterval = 0.9f;
    private double _swingTimer = 0.3f;
    private const float _ATTACK_LAND_FRAME = 0.6f;

    public void Update(Skeleton monster, GameTime time)
    {
        double elapsedTime = time.ElapsedGameTime.TotalSeconds;

        if (monster.State.Value == ActorState.Dead)
        {
            _timeSinceDeath += (float)elapsedTime;

            if (_timeSinceDeath >= _corpseDespawnTime)
            {
                Game1.World.RemoveActor(monster);
            }

            monster.ActionState.Value = ActorActionState.None;
            return;
        }

        float x1 = monster.Position.X;
        float y1 = monster.Position.Y;
        float x2 = Game1.World.Player.Position.X;
        float y2 = Game1.World.Player.Position.Y;

        if (x1 < x2)
        {
            monster.Facing = ActorFacing.Right;
        }
        else if (x1 > x2)
        {
            monster.Facing = ActorFacing.Left;
        }

        double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

        if (distance <= 150)
        {
            monster.IsLeashed = true;
        }

        if (!monster.IsLeashed)
        {
            monster.State.Value = ActorState.Idling;
            return;
        }

        float deltaX = x2 - x1;
        float deltaY = y2 - y1;
        double angle = Math.Atan2(deltaY, deltaX);

        double x = monster.Position.X + (monster.Stats.Speed * elapsedTime * Math.Cos(angle));
        double y = monster.Position.Y + (monster.Stats.Speed * elapsedTime * Math.Sin(angle));

        bool withinAttackHitDistance = distance <= 64;
        bool withinAttackTriggerDistance = distance <= 32;

        if (withinAttackTriggerDistance && !_isAttacking)
        {
            monster.State.Value = ActorState.Idling;
            monster.ActionState.Value = ActorActionState.Swinging;
            _isAttacking = true;
        }

        if (_isAttacking)
        {
            _swingTimer += elapsedTime;

            if (_swingTimer >= _ATTACK_LAND_FRAME)
            {
                // TODO: there should be an attack duration i.e. how long can you get hit for after starting swinging
                if (withinAttackHitDistance && !_playerWasHit)
                {
                    // TODO: proper hitbox checking
                    Game1.World.Player.TakeDamage(10);
                    _playerWasHit = true;
                }
            }

            if (_swingTimer >= _attackInterval)
            {
                _swingTimer = 0f;
                _isAttacking = false;
                _playerWasHit = false;
            }
        }
        else
        {
            monster.Position = new((float)x, (float)y);
            monster.State.Value = ActorState.Walking;
            monster.ActionState.Value = ActorActionState.None;
        }
    }
}
