using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class DroppedItem
{
    public Item Item;
    public Vector2 Position;
    public string DisplayText => GetDisplayName();
    public bool IsHovered = false;

    private Vector2 stringOrigin;
    private Rectangle bounds;

    private string GetDisplayName()
    {
        string displayText = $"{Item.Name}";
        if (Item is MaterialItem materialItem && materialItem.StackQuantity > 1)
        {
            displayText += $" x{materialItem.StackQuantity}";
        }
        return displayText;
    }

    public DroppedItem(Item item, Vector2 position)
    {
        Item = item;
        Position = position;

        stringOrigin = Assets.Fonts.MonogramExtened.MeasureString(DisplayText);

        // TODO: add some padding, decrease if needed to make materials with different stack sizes have same width

        const int HEIGHT = 16;
        int WIDTH = (int)stringOrigin.X + 16;
        bounds = new Rectangle(
            (int)Position.X - (WIDTH / 2),
            (int)Position.Y - (HEIGHT / 2),
            WIDTH,
            HEIGHT
        );
    }

#pragma warning disable IDE0060
    public void Update(GameTime gameTime)
    {
        IsHovered = bounds.Contains(MouseManager.WorldMousePosition);
    }
#pragma warning restore IDE0060


    public void Draw(SpriteBatch spriteBatch)
    {
        const int borderThickness = 1;

        spriteBatch.Draw(Assets.RectangleTexture, bounds, Color.Transparent);

        // Top border
        spriteBatch.Draw(
            Assets.RectangleTexture,
            bounds with
            {
                Height = borderThickness,
            },
            null,
            Item.Rarity.GetColor(),
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.DroppedItemBorder
        );

        // Bottom border
        spriteBatch.Draw(
            Assets.RectangleTexture,
            new Rectangle(
                bounds.X,
                bounds.Y + bounds.Height - borderThickness,
                bounds.Width,
                borderThickness
            ),
            null,
            Item.Rarity.GetColor(),
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.DroppedItemBorder
        );

        // Left border
        spriteBatch.Draw(
            Assets.RectangleTexture,
            bounds with
            {
                Width = borderThickness,
            },
            null,
            Item.Rarity.GetColor(),
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.DroppedItemBorder
        );

        // Right border
        spriteBatch.Draw(
            Assets.RectangleTexture,
            new Rectangle(
                bounds.X + bounds.Width - borderThickness,
                bounds.Y,
                borderThickness,
                bounds.Height
            ),
            null,
            Item.Rarity.GetColor(),
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.DroppedItemBorder
        );

        spriteBatch.Draw(
            Assets.RectangleTexture,
            bounds,
            null,
            IsHovered ? Color.White : new Color(0, 0, 0, 0.6f),
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.DroppedItem
        );

        Color textColor = Item.Rarity.GetColor();
        if (Item.Rarity == Rarity.Normal && IsHovered)
        {
            textColor = Color.Black;
        }

        Game1.DrawText(
            spriteBatch,
            DisplayText,
            new(bounds.X + ((bounds.Width - stringOrigin.X) / 2), bounds.Y),
            Layer.DroppedItemText,
            textColor
        );
    }

    public virtual bool GetPickedUp(Player player)
    {
        bool added = Item.GetPickedUp(player);
        if (added)
            _ = Game1.World.Items.Remove(this);

        return added;
    }
}
