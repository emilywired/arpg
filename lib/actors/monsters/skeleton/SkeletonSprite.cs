using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SkeletonSprite : AnimatedSprite
{
    private TextureAsset idleAsset = Assets.Monsters.Skeleton.Idle;
    private TextureAsset attackAsset = Assets.Monsters.Skeleton.Attack;
    private TextureAsset walkAsset = Assets.Monsters.Skeleton.Walk;
    private TextureAsset deathAsset = Assets.Monsters.Skeleton.Death; // TODO: add one of the two corpse frames
    protected override float FrameTime => 0.15f;

    private Skeleton skeleton;

    public SkeletonSprite(Skeleton skeleton)
    { 
        this.skeleton = skeleton;
        SetTextureAsset(idleAsset);
        
        this.skeleton.State.Connect(onSkeletonStateChanged);
        this.skeleton.ActionState.Connect(onSkeletonStateChanged);
    }

    private void onSkeletonStateChanged()
    {
        SetTextureAsset((skeleton.State.Value, skeleton.ActionState.Value) switch
        {
            // (ActorState.Walking, ActorActionState.Swinging) => _walkAttackAsset,
            (_, ActorActionState.Swinging) => attackAsset,
            (ActorState.Idling, _) => idleAsset,
            (ActorState.Walking, _) => walkAsset,
            (ActorState.Dead, _) => deathAsset,
            _ => throw new SystemException("Unhandled ActorState"),
        });
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        SpriteEffects = skeleton.Facing == ActorFacing.Right
            ? SpriteEffects.None
            : SpriteEffects.FlipHorizontally;

        if (GameState.IsDebugMode)
        {
            if (skeleton.Hitbox is RectangleHitbox rectangleHitbox)
            {
                spriteBatch.Draw(
                    Assets.RectangleTexture,
                    rectangleHitbox.Bounds,
                    null,
                    Color.Yellow,
                    0f,
                    Vector2.Zero,
                    SpriteEffects.None,
                    Layer.Hitbox
                );
            }
            else
            {
                throw new NotImplementedException("Unhandled hitbox type");
            }
        }

        base.Draw(spriteBatch);

        string monsterHealth = $"{Math.Floor(skeleton.Stats.Health)}";
        Vector2 monsterHealthOrigin = Assets.Fonts.MonogramExtened.MeasureString(monsterHealth);

        spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            monsterHealth,
            new((int)skeleton.Position.X, (int)skeleton.Position.Y + 30),
            Color.White,
            0f,
            new((int)(monsterHealthOrigin.X / 2), (int)(monsterHealthOrigin.Y / 2)),
            1f,
            SpriteEffects.None,
            Layer.Text
        );
    }
}
