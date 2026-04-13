using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class PauseMenu
{
    public bool IsDisplayed => !GameState.IsRunning;

    public void Update(GameTime gameTime) { }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!IsDisplayed)
            return;

        string text = "Game Paused";

        Game1.DrawText(
            spriteBatch,
            text,
            new(Game1.NativeResolution.Width / 2, Game1.NativeResolution.Height / 2),
            Layer.Text,
            Color.White,
            center: true
        );
    }

    public bool OnClose()
    {
        GameState.IsRunning = !GameState.IsRunning;
        return true;
    }

    public bool OnLeftClick()
    {
        // TODO: handle left click
        return false;
    }
}
