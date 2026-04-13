using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HolyFireGraphicsComponent
{
    private Texture2D _texture = null!;

    public HolyFireGraphicsComponent(HolyFireEntity holyFire)
    {
        Update(holyFire);
    }

    public void Update(HolyFireEntity holyFire)
    {
        _texture = Assets.CreateCircleTexture((int)holyFire.Radius);
    }

    public void Draw(HolyFireEntity holyFire, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            _texture,
            new(
                (int)(holyFire.Position.X - holyFire.Radius),
                (int)(holyFire.Position.Y - holyFire.Radius)
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
