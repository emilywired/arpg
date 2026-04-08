using System;
using System.Collections.Generic;
using arpg;
using Microsoft.Xna.Framework;

public class PlayerInputComponent
{
    private Player _player;

    public bool IsMoving { get; private set; } = false;
    private bool _isHoldingLeftClick;
    private Vector2 _destination;
    private double _destinationAngle = 0d;
    private double _playerAimAngle = 0d;
    private ISkill? _heldSkill = null;
    private MovementTrack? _movementTrack = null;

    public PlayerInputComponent(Player player)
    {
        _player = player;

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
                    player.Skills.HolyFire.Cast(_playerAimAngle);
                }
            }
        );
    }

    private void HoldSkill(ISkill skill)
    {
        _heldSkill = skill;
    }

    private void ReleaseSkill(ISkill skill)
    {
        if (_heldSkill == skill)
            _heldSkill = null;
    }

    public void Update(GameTime gameTime)
    {
        if (!GameState.IsRunning)
            return;

        // foreach (var transform in _transforms)
        // {
        //     transform.Update(gameTime);
        // }
        // _transforms.RemoveAll(t => t.IsFinished);

        // TODO: prevent clicks outside of window
        _playerAimAngle = CalculateAngle(_player.Position, MouseManager.WorldMousePosition);

        _heldSkill?.Cast(_playerAimAngle);

        if (_isHoldingLeftClick)
            StartMove(MouseManager.WorldMousePosition);

        if (!IsMoving)
            return;

        float distanceToDestination = Vector2.Distance(_player.Position, _destination);
        if (distanceToDestination > 1f)
        {
            double elapsedTime = gameTime.ElapsedGameTime.TotalSeconds;
            float x =
                _player.Position.X
                + (float)(_player.Stats.Speed * elapsedTime * Math.Cos(_destinationAngle));
            float y =
                _player.Position.Y
                + (float)(_player.Stats.Speed * elapsedTime * Math.Sin(_destinationAngle));
            _player.Position = new(x, y);
        }
        else
        {
            _player.Position = _destination;
            IsMoving = false;
            _movementTrack?.InvokeOnComplete();
            _movementTrack = null;
            _player.TransitionState(ActorState.Idling);
        }
    }

    public bool OnLeftClick()
    {
        if (!GameState.IsRunning)
            return false;

        _isHoldingLeftClick = true;
        StartMove(MouseManager.WorldMousePosition);

        return true;
    }

    public bool OnLeftClickRelease()
    {
        _isHoldingLeftClick = false;
        return true;
    }

    public MovementTrack StartMove(Vector2 aimCoordinate)
    {
        // TODO: refactor

        IsMoving = true;
        _destination = aimCoordinate;
        _destinationAngle = CalculateAngle(_player.Position, aimCoordinate);

        double angleInDegrees = MathHelper.ToDegrees((float)_destinationAngle);
        bool isFacingRight = angleInDegrees >= -90 && angleInDegrees <= 90;
        _player.Facing = isFacingRight ? ActorFacing.Right : ActorFacing.Left;
        _player.TransitionState(ActorState.Walking);

        _movementTrack = new();

        return _movementTrack;
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
