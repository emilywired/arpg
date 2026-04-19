using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class XpBar : IHudElement
{
    public int Level { get; private set; }
    public int CurrentXP { get; private set; }
    public int RequiredXP { get; private set; }

    public void Update(GameTime gameTime)
    {
        PlayerStats playerStats = Game1.World.Player.Stats;
        Level = playerStats.Level.Current;
        CurrentXP = playerStats.Level.CurrentXP;
        RequiredXP = playerStats.Level.RequiredXP;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Game1.DrawText(
            spriteBatch,
            $"Lv{Level} {CurrentXP}/{RequiredXP}",
            new Vector2(Game1.NativeResolution.Width / 2, Game1.NativeResolution.Height - 12),
            Layer.Text,
            Color.Yellow,
            center: true
        );
    }
};
