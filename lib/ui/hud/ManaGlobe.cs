using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ManaGlobe : IHudElement
{
    public double Mana { get; set; } = 100;
    public double MaxMana { get; set; } = 100;

    public Vector2 Size { get; set; }
    public Vector2 Position { get; set; }
    public Rectangle Rectangle => new((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    public ManaGlobe()
    {
        Size = new Vector2(55, 55);
        Position = new Vector2(
            Game1.NativeResolution.Width - Size.X,
            Game1.NativeResolution.Height - Size.Y
        );
    }

    public void Update(GameTime gameTime)
    {
        Mana = Math.Floor(Game1.World.Player.Stats.Mana);
        MaxMana = Math.Floor(Game1.World.Player.Stats.MaxMana);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Assets.RectangleTexture,
            Rectangle,
            null,
            new Color(10, 10, 140),
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.HUD
        );


        Game1.DrawText(
            spriteBatch,
            $"{Mana}/{MaxMana}",
            new Vector2(Position.X, Position.Y - 12),
            Layer.Text,
            Color.White
        );
    }
};
