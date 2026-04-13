using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class FrozenOrbBehaviorComponent(FrozenOrbEntity parent) : SkillBehaviorComponent<FrozenOrbEntity>(parent)
{
    private readonly List<FrozenOrbSecondaryEntity> SecondaryEntities = [];
    private float frameTime = 0f;
    private float secondaryProjectileAngle = 0f;
    private float secondaryProjectileInterval = 0.1f;
    private float rotationIncreasePerProjectile = MathHelper.ToRadians(75f);

    public override void Update(float dt)
    {
        base.Update(dt);
        frameTime += dt;

        if (CurrentDuration >= Parent.MaxDuration)
        {
            Parent.Destroy();
            return;
        }

        double x =
            Parent.Position.X + (Parent.Speed * dt * Math.Cos(Parent.Angle));
        double y =
            Parent.Position.Y + (Parent.Speed * dt * Math.Sin(Parent.Angle));
        Parent.Position = new((float)x, (float)y);

        float rotationIncreasePerSecond =
            rotationIncreasePerProjectile / secondaryProjectileInterval;
        secondaryProjectileAngle += (rotationIncreasePerSecond * dt) % 360;

        if (frameTime >= secondaryProjectileInterval)
        {
            int offset = 16;
            Vector2 position = new(
                Parent.Position.X + (offset * (float)Math.Cos(secondaryProjectileAngle)),
                Parent.Position.Y + (offset * (float)Math.Sin(secondaryProjectileAngle))
            );

            FrozenOrbSecondaryEntity secondaryEntity = new(Parent.Owner)
            {
                Position = position,
                Angle = secondaryProjectileAngle,
            };
            SecondaryEntities.Add(secondaryEntity);

            frameTime -= secondaryProjectileInterval;
        }
    }
}
