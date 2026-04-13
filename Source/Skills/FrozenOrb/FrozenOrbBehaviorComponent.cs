using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class FrozenOrbBehaviorComponent
{
    public float CurrentDuration = 0f;
    private readonly List<FrozenOrbSecondaryEntity> SecondaryEntities = [];
    private float _frameTime = 0f;
    private float _secondaryProjectileAngle = 0f;
    private float _secondaryProjectileInterval = 0.1f;
    private float _rotationIncreasePerProjectile = MathHelper.ToRadians(75f);

    public void Update(FrozenOrbEntity frozenOrb, GameTime gameTime)
    {
        var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _frameTime += elapsedTime;
        CurrentDuration += elapsedTime;

        if (CurrentDuration >= frozenOrb.MaxDuration)
        {
            frozenOrb.Destroy();
            return;
        }

        double x =
            frozenOrb.Position.X + (frozenOrb.Speed * elapsedTime * Math.Cos(frozenOrb.Angle));
        double y =
            frozenOrb.Position.Y + (frozenOrb.Speed * elapsedTime * Math.Sin(frozenOrb.Angle));
        frozenOrb.Position = new((float)x, (float)y);

        float rotationIncreasePerSecond =
            _rotationIncreasePerProjectile / _secondaryProjectileInterval;
        _secondaryProjectileAngle += (rotationIncreasePerSecond * elapsedTime) % 360;

        if (_frameTime >= _secondaryProjectileInterval)
        {
            int offset = 16;
            Vector2 position = new(
                frozenOrb.Position.X + offset * (float)Math.Cos(_secondaryProjectileAngle),
                frozenOrb.Position.Y + offset * (float)Math.Sin(_secondaryProjectileAngle)
            );

            FrozenOrbSecondaryEntity secondaryEntity = new()
            {
                Position = position,
                Angle = _secondaryProjectileAngle,
            };
            SecondaryEntities.Add(secondaryEntity);

            _frameTime -= _secondaryProjectileInterval;
        }
    }
}
