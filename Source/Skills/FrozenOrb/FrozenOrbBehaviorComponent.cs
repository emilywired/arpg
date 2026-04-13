using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class FrozenOrbBehaviorComponent
{
    public float CurrentDuration = 0f;
    private readonly List<FrozenOrbSecondaryEntity> SecondaryEntities = [];
    private float frameTime = 0f;
    private float secondaryProjectileAngle = 0f;
    private float secondaryProjectileInterval = 0.1f;
    private float rotationIncreasePerProjectile = MathHelper.ToRadians(75f);

    public void Update(FrozenOrbEntity frozenOrb, GameTime gameTime)
    {
        float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        frameTime += elapsedTime;
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
            rotationIncreasePerProjectile / secondaryProjectileInterval;
        secondaryProjectileAngle += rotationIncreasePerSecond * elapsedTime % 360;

        if (frameTime >= secondaryProjectileInterval)
        {
            int offset = 16;
            Vector2 position = new(
                frozenOrb.Position.X + (offset * (float)Math.Cos(secondaryProjectileAngle)),
                frozenOrb.Position.Y + (offset * (float)Math.Sin(secondaryProjectileAngle))
            );

            FrozenOrbSecondaryEntity secondaryEntity = new()
            {
                Position = position,
                Angle = secondaryProjectileAngle,
            };
            SecondaryEntities.Add(secondaryEntity);

            frameTime -= secondaryProjectileInterval;
        }
    }
}
