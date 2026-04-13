
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameUI
{
    private InventoryUI inventoryUI;

    public GameUI(Player player)
    {
        inventoryUI = new InventoryUI(player);
        Game1.InputManager.OnPress(RemappableGameAction.OpenInventory, ToggleInventory);
    }

    public void Update(GameTime gameTime)
    {
        inventoryUI.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        inventoryUI.Draw(spriteBatch);
    }

    public void ToggleInventory()
    {
        if (GameState.IsRunning)
            inventoryUI.IsOpen = !inventoryUI.IsOpen;
    }

    public bool OnClose()
    {
        if (inventoryUI.IsOpen)
        {
            inventoryUI.IsOpen = false;
            return true;
        }
        return false;
    }

    public bool OnLeftClick()
    {
        Vector2 mousePosition = MouseManager.ScreenMousePosition;

        if (inventoryUI.OnLeftClick(mousePosition))
            return true;

        return false;
    }

    public bool OnRightClick()
    {
        if (inventoryUI.OnRightClick())
            return true;

        return false;
    }
}
