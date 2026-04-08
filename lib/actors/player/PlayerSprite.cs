using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class PlayerSprite : AnimatedSprite
{
    private Player player;
    private TextureAsset idleAsset = Assets.Player.Idle;
    private TextureAsset walkAsset = Assets.Player.Walk;

    public PlayerSprite(Player player)
    {
        this.player = player;
        SetTextureAsset(idleAsset);

        this.player.OnStateChanged += () => {
            SetTextureAsset(this.player.State switch {
                ActorState.Idling => idleAsset,
                ActorState.Walking => walkAsset,
                _ => throw new SystemException("Unhandled ActorState"),
            });
        };
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        SpriteEffects = player.Facing == ActorFacing.Right
            ? SpriteEffects.None
            : SpriteEffects.FlipHorizontally;

        if (GameState.IsDebugMode)
        {
            if (player.Hitbox is RectangleHitbox rectangleHitbox)
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
    }
}
