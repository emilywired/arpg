using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Stash(Vector2 position)
{
    public bool IsOpen { get; private set; } = false;
    public bool IsHovered { get; private set; } = false;
    public readonly Vector2 Position = position;
    public readonly Rectangle Bounds = new((int)position.X, (int)position.Y, 32, 32);

    private TextureAsset asset = Assets.Environment.Chest;
    private int currentFrame => IsOpen ? 1 : 0;

    public bool OnLeftClick()
    {
        // TODO: approach chest then open if uninterrupted
        IsOpen = true;
        return false;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            asset.Texture,
            Bounds,
            asset.Frames[currentFrame],
            Color.White,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.Stash
        );
    }

    public void Update(GameTime gameTime)
    {
        IsHovered = Bounds.Contains(MouseManager.WorldMousePosition);
    }
}
