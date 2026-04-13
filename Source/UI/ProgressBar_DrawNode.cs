using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ProgressBarDrawNode(ProgressBar source) : DrawNode<ProgressBar>(source)
{
    public override void Draw(SpriteBatch spriteBatch)
    {
        var targetRect = new Rectangle(
            (int)(Source.GetDrawPosition().X - (Source.CenterHorizontally ? Source.Size.X / 2 : Source.Size.X)),
            (int)Source.GetDrawPosition().Y,
            (int)Source.Size.X,
            (int)Source.Size.Y
        );

        spriteBatch.Draw(
            Assets.RectangleTexture,
            targetRect,
            null,
            Source.BackgroundColor,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.Text
        );

        Rectangle destinationRectangle = Source.IsVertical
            ? targetRect with
            {
                Width = (int)(Source.Size.X * Source.Progress),
            }
            : targetRect with
            {
                Y = (int)(targetRect.Y + (Source.Size.Y * (1 - Source.Progress))),
                Height = (int)(Source.Size.Y * Source.Progress),
            };

        spriteBatch.Draw(
            Assets.RectangleTexture,
            destinationRectangle,
            null,
            Source.Color,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.DroppedItem
        );

        if (Source.ShowText)
        {
            var textPosition = new Vector2(
                (int)(targetRect.X + (targetRect.Width / 2f)),
                (int)(targetRect.Y + (float)Source.VerticalTextOffset + (targetRect.Height / 2f))
            );

            string text = $"{Source.Value} / {Source.MaxValue}";
            Vector2 textMeasurement = Assets.Fonts.MonogramExtened.MeasureString(text);

            spriteBatch.DrawString(
                Assets.Fonts.MonogramExtened,
                text,
                textPosition,
                Source.TextColor,
                0f,
                new((int)(textMeasurement.X / 2), (int)(textMeasurement.Y / 2)),
                1f,
                SpriteEffects.None,
                Layer.DroppedItem
            );
        }
    }
}