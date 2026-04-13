using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Config
{
    public int Scale { get; private set; }
    public float ScaleX { get; private set; }
    public float ScaleY { get; private set; }
    public bool DisplayEnemyHealthBars { get; set; } = true;
    private GraphicsDeviceManager _graphics;
    private GraphicsDevice _device;
    private RenderTarget2D _renderTarget;

    public Config(
        GraphicsDeviceManager graphics,
        GraphicsDevice device,
        RenderTarget2D renderTarget
    )
    {
        _graphics = graphics;
        _device = device;
        _renderTarget = renderTarget;
        ChangeResolutionScale(3);
        SetFullScreen();
        ApplyChanges();
    }

    public void ChangeResolutionScale(int scale)
    {
        _graphics.PreferredBackBufferWidth = Game1.NativeResolution.Width * scale;
        _graphics.PreferredBackBufferHeight = Game1.NativeResolution.Height * scale;
        Scale = scale;
    }

    public void ToggleFullScreen()
    {
        _graphics.IsFullScreen = !_graphics.IsFullScreen;
    }

    public void SetFullScreen()
    {
        _graphics.IsFullScreen = true;
    }

    public void SetMinimizedScreen()
    {
        _graphics.IsFullScreen = false;
    }

    public void ApplyChanges()
    {
        _graphics.ApplyChanges();
        ScaleX = (float)_device.Viewport.Width / _renderTarget.Width;
        ScaleY = (float)_device.Viewport.Height / _renderTarget.Height;
    }
}
