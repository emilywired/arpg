using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ProgressBar
{
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }

    public Color Color { get; set; } = Color.CornflowerBlue;
    public Color BackgroundColor { get; set; } = Color.Black;
    public Color TextColor { get; set; } = Color.White;

    public bool ShowText { get; set; }

    public double Value { get; set; } = 50;
    public double MaxValue { get; set; } = 100;

    public double Progress
    {
        get => MaxValue != 0 ? Value / MaxValue : 0;
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        var targetRect = new Rectangle((int)(Position.X - Size.X / 2), (int)Position.Y, (int)Size.X, (int)Size.Y);
        spriteBatch.Draw(
            Assets.RectangleTexture,
            targetRect,
            null,
            BackgroundColor,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.Text
        );

        spriteBatch.Draw(
            Assets.RectangleTexture,
            targetRect with { Width = (int)(Size.X * Progress) },
            null,
            Color,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.DroppedItem
        );

        if (ShowText)
        {
            string text = $"{Value} / {MaxValue}";
            Vector2 textMeasurement = Assets.Fonts.MonogramExtened.MeasureString(text);

            spriteBatch.DrawString(
                Assets.Fonts.MonogramExtened,
                text,
                new((int)Position.X, (int)Position.Y),
                TextColor,
                0f,
                new((int)(textMeasurement.X / 2), (int)(textMeasurement.Y / 2)),
                1f,
                SpriteEffects.None,
                Layer.DroppedItem
            );
        }
    }
}