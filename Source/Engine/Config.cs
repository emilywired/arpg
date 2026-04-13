using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Config
{
    public int Scale { get; private set; }
    public float ScaleX { get; private set; }
    public float ScaleY { get; private set; }
    public bool DisplayEnemyHealthBars { get; set; } = true;

    private GraphicsDeviceManager graphics;
    private GraphicsDevice device;
    private RenderTarget2D renderTarget;

    public Config(
        GraphicsDeviceManager _graphics,
        GraphicsDevice _device,
        RenderTarget2D _renderTarget
    )
    {
        graphics = _graphics;
        device = _device;
        renderTarget = _renderTarget;
        ChangeResolutionScale(3);
        SetFullScreen();
        ApplyChanges();
    }

    public void ChangeResolutionScale(int scale)
    {
        graphics.PreferredBackBufferWidth = Game1.NativeResolution.Width * scale;
        graphics.PreferredBackBufferHeight = Game1.NativeResolution.Height * scale;
        Scale = scale;
    }

    public void ToggleFullScreen()
    {
        graphics.IsFullScreen = !graphics.IsFullScreen;
    }

    public void SetFullScreen()
    {
        graphics.IsFullScreen = true;
    }

    public void SetMinimizedScreen()
    {
        graphics.IsFullScreen = false;
    }

    public void ApplyChanges()
    {
        graphics.ApplyChanges();
        ScaleX = (float)device.Viewport.Width / renderTarget.Width;
        ScaleY = (float)device.Viewport.Height / renderTarget.Height;
    }
}
