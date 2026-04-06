using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Item
{
    public bool IsHovered { get; set; } = false;
    public string Name { get; private set; }
    public Rarity Rarity { get; protected set; }
    public bool IsCorrupted { get; private set; } = false;
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Asset Asset;

    private const int ITEM_1x1_SIZE = 20;

    public Item(string name, Rarity rarity, int width, int height, Asset asset)
    {
        Name = name;
        Rarity = rarity;
        Width = width;
        Height = height;
        Asset = asset;
    }

    public void Draw(SpriteBatch spriteBatch, int x, int y)
    {
        spriteBatch.Draw(
            Asset.Texture,
            new(x, y, Width * ITEM_1x1_SIZE, Height * ITEM_1x1_SIZE),
            null,
            Color.White,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.Item
        );
    }

    public void DrawTooltip(SpriteBatch spriteBatch)
    {
        ItemTooltip.Instance.SetItem(this);
        ItemTooltip.Instance.Draw(spriteBatch);
    }
}
