using System.Collections.Generic;
using System.Linq;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class InventoryUI
{
    public bool IsOpen = false;
    public Rectangle WindowBounds;
    public Item? HoveredItem;

    private Player _player;
    private const int SQUARE_SIZE = 20;
    private const int INVENTORY_BORDER_SIZE = 1;
    private const int EQUIPMENT_BORDER_SIZE = 3;
    private List<Rectangle> _inventorySquaresBounds = [];
    private List<Rectangle> _equipmentBounds = [];
    private List<EquippableItem> _equipmentItems;

    private Dictionary<Rectangle, (int i, int j)> _inventorySquaresToGridPositions = [];

    public InventoryUI(Player player)
    {
        _player = player;
        int windowWidth = Game1.NativeResolution.Width / 2 - 70;
        int screenWidth = Game1.NativeResolution.Width;
        int screenHeight = Game1.NativeResolution.Height;
        WindowBounds = new(screenWidth - windowWidth, 0, windowWidth, screenHeight);

        _equipmentItems = new()
        {
            _player.Equipment.MainHand,
            _player.Equipment.Gloves,
            _player.Equipment.LeftRing,
            _player.Equipment.Belt,
            _player.Equipment.Chest,
            _player.Equipment.Head,
            _player.Equipment.RightRing,
            _player.Equipment.Amulet,
            _player.Equipment.Boots,
            _player.Equipment.OffHand,
        };

        InitInventory();
        InitEquipment();
    }

    public void Update(GameTime gameTime)
    {
        Vector2 mousePosition = MouseManager.GetInGameMousePosition();
        FindHoveredItem(mousePosition);
    }

    private void FindHoveredItem(Vector2 mousePosition)
    {
        foreach (Rectangle bounds in _inventorySquaresBounds)
        {
            Rectangle boundsWidthBorder = new(
                bounds.X,
                bounds.Y,
                bounds.Width + INVENTORY_BORDER_SIZE,
                bounds.Height + INVENTORY_BORDER_SIZE
            );

            if (boundsWidthBorder.Contains(mousePosition))
            {
                var (i, j) = _inventorySquaresToGridPositions[bounds];
                Item? item = _player.Inventory.GetItem(i, j);
                if (item is not null)
                {
                    HoveredItem = item;
                    return;
                }
            }
        }

        for (int i = 0; i < _equipmentBounds.Count; i++)
        {
            Rectangle bounds = _equipmentBounds[i];
            if (bounds.Contains(mousePosition))
            {
                EquippableItem? item = _equipmentItems[i];
                if (item is not null)
                {
                    HoveredItem = item;
                    return;
                }
            }
        }

        HoveredItem = null;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!IsOpen)
            return;

        DrawWindow(spriteBatch);
        DrawEquipment(spriteBatch);
        DrawInventory(spriteBatch);
        DrawTooltip(spriteBatch);
    }

    public bool OnLeftClick(Vector2 mousePosition)
    {
        if (!IsOpen)
            return false;

        bool cursorWithin_windowBounds =
            mousePosition.X > WindowBounds.Left
            && mousePosition.X < WindowBounds.Right
            && mousePosition.Y > WindowBounds.Top
            && mousePosition.Y < WindowBounds.Bottom;

        if (!cursorWithin_windowBounds)
            return false;

        return true;
    }

    public bool OnRightClick()
    {
        if (!IsOpen)
            return false;

        if (HoveredItem is not null && HoveredItem is EquippableItem equippable)
        {
            // TODO: flowchart and implementation of all scenarios

            // TODO: check whether HoveredItem is in inventory or in equipment
            _player.Equipment.Equip(equippable);
            _player.Inventory.RemoveItem(equippable);
            return true;
        }

        return false;
    }

    private void DrawWindow(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Assets.RectangleTexture,
            WindowBounds,
            null,
            new Color(46, 54, 53),
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.UIWindow
        );
    }

    private void InitEquipment()
    {
        int equipmentWidth = SQUARE_SIZE * 8 + EQUIPMENT_BORDER_SIZE * 4;

        int currentX = WindowBounds.Left + ((WindowBounds.Width - equipmentWidth) / 2);
        int currentY = 10;

        // main hand
        _equipmentBounds.Add(new(currentX, currentY, SQUARE_SIZE * 2, SQUARE_SIZE * 4));

        currentY += SQUARE_SIZE * 4 + EQUIPMENT_BORDER_SIZE;

        // gloves
        _equipmentBounds.Add(new(currentX, currentY, SQUARE_SIZE * 2, SQUARE_SIZE * 2));
        currentY += SQUARE_SIZE;
        currentX += SQUARE_SIZE * 2 + EQUIPMENT_BORDER_SIZE;

        // left ring
        _equipmentBounds.Add(new(currentX, currentY, SQUARE_SIZE * 1, SQUARE_SIZE * 1));
        currentX += SQUARE_SIZE + EQUIPMENT_BORDER_SIZE;

        // belt
        _equipmentBounds.Add(new(currentX, currentY, SQUARE_SIZE * 2, SQUARE_SIZE * 1));
        currentY -= SQUARE_SIZE * 3 + EQUIPMENT_BORDER_SIZE;

        // chest
        _equipmentBounds.Add(new(currentX, currentY, SQUARE_SIZE * 2, SQUARE_SIZE * 3));
        currentY -= SQUARE_SIZE * 2 + EQUIPMENT_BORDER_SIZE;

        // helmet
        _equipmentBounds.Add(new(currentX, currentY, SQUARE_SIZE * 2, SQUARE_SIZE * 2));
        currentY += SQUARE_SIZE * 5 + EQUIPMENT_BORDER_SIZE * 2;
        currentX += SQUARE_SIZE * 2 + EQUIPMENT_BORDER_SIZE;

        // right ring
        _equipmentBounds.Add(new(currentX, currentY, SQUARE_SIZE * 1, SQUARE_SIZE * 1));
        currentY -= (int)(SQUARE_SIZE * 3.5) + EQUIPMENT_BORDER_SIZE * 2;

        // amulet
        _equipmentBounds.Add(new(currentX, currentY, SQUARE_SIZE * 1, SQUARE_SIZE * 1));
        currentY += (int)(SQUARE_SIZE * 3.5) + EQUIPMENT_BORDER_SIZE * 2;
        currentX += SQUARE_SIZE + EQUIPMENT_BORDER_SIZE;
        currentY -= SQUARE_SIZE;

        // boots
        _equipmentBounds.Add(new(currentX, currentY, SQUARE_SIZE * 2, SQUARE_SIZE * 2));
        currentY -= SQUARE_SIZE * 4 + EQUIPMENT_BORDER_SIZE;

        // offhand
        _equipmentBounds.Add(new(currentX, currentY, SQUARE_SIZE * 2, SQUARE_SIZE * 4));
    }

    private void DrawEquipment(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < _equipmentBounds.Count; i++)
        {
            Rectangle bounds = _equipmentBounds[i];
            DrawSquare(spriteBatch, bounds);
            _equipmentItems[i]?.Draw(spriteBatch, bounds.X, bounds.Y);
        }

        string goldAmountText = $"Gold: {_player.Gold}";

        Game1.DrawText(
            spriteBatch,
            goldAmountText,
            new Vector2(WindowBounds.Left + 4, 240),
            Layer.UIWindowElementText,
            Color.Yellow
        );
    }

    private void InitInventory()
    {
        int inventoryHeight = _player.Inventory.Height * (SQUARE_SIZE + INVENTORY_BORDER_SIZE) - 1;
        int inventoryWidth = _player.Inventory.Width * (SQUARE_SIZE + INVENTORY_BORDER_SIZE) - 1;

        for (int i = 0; i < _player.Inventory.Width; i++)
        {
            for (int j = 0; j < _player.Inventory.Height; j++)
            {
                int x = WindowBounds.Left + i * (SQUARE_SIZE + INVENTORY_BORDER_SIZE);
                int y =
                    WindowBounds.Bottom
                    - inventoryHeight
                    + j * (SQUARE_SIZE + INVENTORY_BORDER_SIZE);

                Rectangle bounds = new(x, y, SQUARE_SIZE, SQUARE_SIZE);
                _inventorySquaresBounds.Add(bounds);
                _inventorySquaresToGridPositions.Add(bounds, (i, j));
            }
        }
    }

    private void DrawInventory(SpriteBatch spriteBatch)
    {
        foreach (Rectangle bounds in _inventorySquaresBounds)
        {
            DrawSquare(spriteBatch, bounds);
            var (i, j) = _inventorySquaresToGridPositions[bounds];
            Item? item = _player.Inventory.GetItem(i, j);
            if (item is not null && _player.Inventory.Grid.SquareIsOriginSquare(i, j))
            {
                item.Draw(spriteBatch, bounds.X, bounds.Y);
            }
        }
    }

    private void DrawSquare(SpriteBatch spriteBatch, Rectangle rectangle)
    {
        spriteBatch.Draw(
            Assets.RectangleTexture,
            rectangle,
            null,
            new Color(8, 8, 30),
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.UIWindowElement
        );
    }

    private void DrawTooltip(SpriteBatch spriteBatch)
    {
        HoveredItem?.DrawTooltip(spriteBatch);
    }
}
