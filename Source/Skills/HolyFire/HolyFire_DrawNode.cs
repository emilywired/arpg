using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HolyFireDrawNode(HolyFireEntity source) : DrawNode<HolyFireEntity>(source)
{
    public override void Draw(SpriteBatch spriteBatch)
    {
        if (Source.Texture == null)
            throw new Exception();

        spriteBatch.Draw(
            Source.Texture,
            new(
                (int)(Source.Position.X - Source.Radius),
                (int)(Source.Position.Y - Source.Radius)
            ),
            null,
            new Color(205, 45, 10, 64),
            0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            Layer.PlayerOnGroundEffect
        );
    }
}
