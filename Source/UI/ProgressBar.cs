using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ProgressBar : IDrawable
{
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }

    public Color Color { get; set; } = Color.CornflowerBlue;
    public Color BackgroundColor { get; set; } = Color.Black;
    public Color TextColor { get; set; } = Color.White;

    public bool IsVertical { get; set; } = true;
    public bool CenterHorizontally { get; set; } = false;
    public bool ShowText { get; set; }
    public double VerticalTextOffset { get; set; } = 0;

    public double Value { get; set; } = 50;
    public double MaxValue { get; set; } = 100;

    public double Progress => MaxValue != 0 ? Value / MaxValue : 0;

    public DrawNode CreateDrawNode()
        => new ProgressBarDrawNode(this);
}
