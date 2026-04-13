using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ProgressBar
{
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }

    public Color Color { get; set; } = Color.CornflowerBlue;
    public Color BackgroundColor { get; set; } = Color.Black;
    public Color TextColor { get; set; } = Color.White;

    public bool IsVertical { get; set; } = true;
    public bool CenterHorizontally { get; set; } = false;
    public bool ShowText { get; set; }
    public double VerticalTextOffset { get; set; } = 0;

    public double Value { get; set; } = 50;
    public double MaxValue { get; set; } = 100;

    public double Progress
    {
        get => MaxValue != 0 ? Value / MaxValue : 0;
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        var targetRect = new Rectangle(
            (int)(Position.X - (CenterHorizontally ? Size.X / 2 : Size.X)),
            (int)Position.Y,
            (int)Size.X,
            (int)Size.Y
        );

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

        Rectangle destinationRectangle = IsVertical
            ? targetRect with
            {
                Width = (int)(Size.X * Progress),
            }
            : targetRect with
            {
                Y = (int)(targetRect.Y + Size.Y * (1 - Progress)),
                Height = (int)(Size.Y * Progress),
            };

        spriteBatch.Draw(
            Assets.RectangleTexture,
            destinationRectangle,
            null,
            Color,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.DroppedItem
        );

        if (ShowText)
        {
            Vector2 textPosition = new Vector2(
                (int)(targetRect.X + targetRect.Width / 2f),
                (int)(targetRect.Y + (float)VerticalTextOffset + targetRect.Height / 2f)
            );

            string text = $"{Value} / {MaxValue}";
            Vector2 textMeasurement = Assets.Fonts.MonogramExtened.MeasureString(text);

            spriteBatch.DrawString(
                Assets.Fonts.MonogramExtened,
                text,
                textPosition,
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
