
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class DroppedItem
{
    public Item Item;
    public Vector2 Position;
    public string DisplayText => GetDisplayName();
    public bool IsHovered = false;
    private Vector2 _stringOrigin;
    private Rectangle _bounds;

    private string GetDisplayName()
    {
        string displayText = $"{Item.Name}";
        if (Item is MaterialItem materialItem && materialItem.StackQuantity > 0)
        {
            displayText += $" x{materialItem.StackQuantity}";
        }
        return displayText;
    }

    public DroppedItem(Item item, Vector2 position)
    {
        Item = item;
        Position = position;

        _stringOrigin = Assets.Fonts.MonogramExtened.MeasureString(DisplayText);

        // TODO: add some padding, decrease if needed to make materials with different stack sizes have same width

        const int HEIGHT = 16;
        int WIDTH = (int)_stringOrigin.X + 16;
        _bounds = new Rectangle(
            (int)Position.X - WIDTH / 2,
            (int)Position.Y - HEIGHT / 2,
            WIDTH,
            HEIGHT
        );
    }

    public void Update(GameTime gameTime)
    {
        Vector2 playerAimCoordinate = MouseManager.WorldMousePosition;
        IsHovered = _bounds.Contains(MouseManager.WorldMousePosition);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        const int borderThickness = 1;

        spriteBatch.Draw(Assets.RectangleTexture, _bounds, Color.Transparent);

        // Top border
        spriteBatch.Draw(
            Assets.RectangleTexture,
            new Rectangle((int)_bounds.X, (int)_bounds.Y, _bounds.Width, borderThickness),
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
                (int)_bounds.X,
                (int)_bounds.Y + _bounds.Height - borderThickness,
                _bounds.Width,
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
            new Rectangle((int)_bounds.X, (int)_bounds.Y, borderThickness, _bounds.Height),
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
                (int)_bounds.X + _bounds.Width - borderThickness,
                (int)_bounds.Y,
                borderThickness,
                _bounds.Height
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
            _bounds,
            null,
            IsHovered ? Color.White : new Color(0, 0, 0, 0.6f),
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.DroppedItem
        );

        Color textColor = Item.Rarity.GetColor();
        if (Item.Rarity == Rarity.Normal && this.IsHovered)
        {
            textColor = Color.Black;
        }

        Game1.DrawText(
            spriteBatch,
            DisplayText,
            new((int)_bounds.X + (_bounds.Width - _stringOrigin.X) / 2, (int)_bounds.Y),
            Layer.DroppedItemText,
            textColor
        );
    }

    public virtual bool GetPickedUp(Player player)
    {
        bool added = Item.GetPickedUp(player);
        if (added)
            Game1.World.Items.Remove(this);

        return added;
    }
}
