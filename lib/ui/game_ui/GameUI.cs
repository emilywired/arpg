using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameUI
{
    private InventoryUI _inventoryUI;

    public GameUI(Player player)
    {
        _inventoryUI = new InventoryUI(player);
        Game1.InputManager.OnPress(RemappableGameAction.OpenInventory, ToggleInventory);
    }

    public void Update(GameTime gameTime)
    {
        _inventoryUI.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _inventoryUI.Draw(spriteBatch);
    }

    public void ToggleInventory()
    {
        if (GameState.IsRunning)
            _inventoryUI.IsOpen = !_inventoryUI.IsOpen;
    }

    public bool OnClose()
    {
        if (_inventoryUI.IsOpen)
        {
            _inventoryUI.IsOpen = false;
            return true;
        }
        return false;
    }

    public bool OnLeftClick()
    {
        Vector2 mousePosition = MouseManager.ScreenMousePosition;

        if (_inventoryUI.OnLeftClick(mousePosition))
            return true;

        return false;
    }

    public bool OnRightClick()
    {
        if (_inventoryUI.OnRightClick())
            return true;

        return false;
    }
}
