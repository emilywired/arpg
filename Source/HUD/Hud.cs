using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Hud
{
    private List<IHudElement> elements = [];

    public Hud()
    {
        elements.Add(new HealthGlobe());
        elements.Add(new ManaGlobe());
        elements.Add(new XpBar());
        elements.Add(new DebugScreen());
    }

    public void Update(GameTime gameTime)
    {
        foreach (IHudElement element in elements)
        {
            element.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (IHudElement element in elements)
        {
            element.Draw(spriteBatch);
        }
    }
}
