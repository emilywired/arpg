using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SkeletonGraphicsComponent : AnimatedSprite
{
    private TextureAsset idleAsset = Assets.Monsters.Skeleton.Idle;
    private TextureAsset attackAsset = Assets.Monsters.Skeleton.Attack;
    private TextureAsset walkAsset = Assets.Monsters.Skeleton.Walk;
    private TextureAsset deathAsset = Assets.Monsters.Skeleton.Death; // TODO: add one of the two corpse frames

    private ProgressBar healthBar;

    private Skeleton skeleton;

    public SkeletonGraphicsComponent(Skeleton skeleton)
    { 
        this.skeleton = skeleton;
        SetTextureAsset(idleAsset);
        FrameTime = 0.15f;

        healthBar = new ProgressBar()
        {
            MaxValue = skeleton.Stats.MaxHealth,
            Size = new(32, 4),
        };

        skeleton.Stats.Health.Connect(this, value => healthBar.Value = value);
        
        this.skeleton.State.Connect(this, onSkeletonStateChanged);
        this.skeleton.ActionState.Connect(this, onSkeletonStateChanged);
    }

    private void onSkeletonStateChanged()
    {
        SetTextureAsset((skeleton.State.Value, skeleton.ActionState.Value) switch
        {
            // (ActorState.Walking, ActorActionState.Swinging) => _walkAttackAsset,
            (ActorState.Dead, _) => deathAsset,
            (_, ActorActionState.Swinging) => attackAsset,
            (ActorState.Idling, _) => idleAsset,
            (ActorState.Walking, _) => walkAsset,
            _ => throw new SystemException("Unhandled ActorState"),
        });
    }

    public override void Update(GameTime gameTime)
    {
        healthBar.Position = Position - new Vector2(0, 20);

        base.Update(gameTime);
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

        if (skeleton.IsAlive)
            healthBar.Draw(spriteBatch);
    }
}
