using System;
using Microsoft.Xna.Framework;

public class PlayerInputComponent
{
    private Player player;

    public bool IsMoving { get; private set; } = false;
    private bool isHoldingLeftClick;
    private Vector2 destination;
    private double destinationAngle = 0d;
    private double playerAimAngle = 0d;
    private ISkill? heldSkill = null;
    private MovementTrack? movementTrack = null;

    public PlayerInputComponent(Player _player)
    {
        player = _player;

        // TODO: refactor
        Game1.InputManager.OnPress(
            RemappableGameAction.CastBarOne,
            () => HoldSkill(player.Skills.Fireball)
        );
        Game1.InputManager.OnRelease(
            RemappableGameAction.CastBarOne,
            () => ReleaseSkill(player.Skills.Fireball)
        );

        Game1.InputManager.OnPress(
            RemappableGameAction.CastBarTwo,
            () => HoldSkill(player.Skills.FrozenOrb)
        );
        Game1.InputManager.OnRelease(
            RemappableGameAction.CastBarTwo,
            () => ReleaseSkill(player.Skills.FrozenOrb)
        );

        // TODO: figure out what the behavior for instant/toggle skills should be
        Game1.InputManager.OnPress(
            RemappableGameAction.CastBarThree,
            () =>
            {
                if (GameState.IsRunning)
                {
                    player.Skills.HolyFire.Cast(playerAimAngle);
                }
            }
        );
    }

    private void HoldSkill(ISkill skill)
    {
        heldSkill = skill;
    }

    private void ReleaseSkill(ISkill skill)
    {
        if (heldSkill == skill)
            heldSkill = null;
    }

    public void Update(float dt)
    {
        if (!GameState.IsRunning)
            return;

        // foreach (var transform in transforms)
        // {
        //     transform.Update(gameTime);
        // }
        // transforms.RemoveAll(t => t.IsFinished);

        // TODO: prevent clicks outside of window
        playerAimAngle = CalculateAngle(player.Position, MouseManager.WorldMousePosition);

        heldSkill?.Cast(playerAimAngle);

        if (isHoldingLeftClick)
            _ = StartMove(MouseManager.WorldMousePosition);

        if (!IsMoving)
            return;

        float distanceToDestination = Vector2.Distance(player.Position, destination);
        if (distanceToDestination > 1f)
        {
            float x =
                player.Position.X
                + (float)(player.Stats.Speed * dt * Math.Cos(destinationAngle));
            float y =
                player.Position.Y
                + (float)(player.Stats.Speed * dt * Math.Sin(destinationAngle));
            player.Position = new(x, y);
        }
        else
        {
            player.Position = destination;
            IsMoving = false;
            movementTrack?.InvokeOnComplete();
            movementTrack = null;
            player.State.Value = ActorState.Idling;
        }
    }

    public bool OnLeftClick()
    {
        if (!GameState.IsRunning)
            return false;

        isHoldingLeftClick = true;
        _ = StartMove(MouseManager.WorldMousePosition);

        return true;
    }

    public bool OnLeftClickRelease()
    {
        isHoldingLeftClick = false;
        return true;
    }

    public MovementTrack StartMove(Vector2 aimCoordinate)
    {
        // TODO: refactor

        IsMoving = true;
        destination = aimCoordinate;
        destinationAngle = CalculateAngle(player.Position, aimCoordinate);

        double angleInDegrees = MathHelper.ToDegrees((float)destinationAngle);
        bool isFacingRight = angleInDegrees is >= -90 and <= 90;
        player.Facing = isFacingRight ? ActorFacing.Right : ActorFacing.Left;
        player.State.Value = ActorState.Walking;

        return movementTrack = new();
    }

    private double CalculateAngle(Vector2 a, Vector2 b)
    {
        float deltaX = b.X - a.X;
        float deltaY = b.Y - a.Y;
        return Math.Atan2(deltaY, deltaX);
    }

    public class MovementTrack
    {
        public event Action? OnComplete;

        internal void InvokeOnComplete() => OnComplete?.Invoke();
    }
}
