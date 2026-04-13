using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HealthGlobe : IHudElement
{
    public Vector2 Size { get; set; }
    public Vector2 Position { get; set; }
    public Rectangle Rectangle => new((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    private ProgressBar progressBar;

    public HealthGlobe()
    {
        Vector2 Size = new(60, 60);
        Position = new Vector2(Size.X, Game1.NativeResolution.Height - Size.Y);

        progressBar = new ProgressBar()
        {
            Size = Size,
            Position = Position,
            Value = Game1.World.Player.Stats.Health.Value,
            MaxValue = Game1.World.Player.Stats.MaxHealth.Value,
            Color = Colors.Health,
            ShowText = true,
            VerticalTextOffset = -((Size.Y / 2) + 10),
            IsVertical = false,
        };

        Game1.World.Player.Stats.Health.Connect(this, value => progressBar.Value = (int)value);
        Game1.World.Player.Stats.MaxHealth.Connect(
            this,
            value => progressBar.MaxValue = (int)value
        );
    }

    public void Update(GameTime gameTime) { }

    public void Draw(SpriteBatch spriteBatch)
    {
        progressBar.Draw(spriteBatch);
    }
};
