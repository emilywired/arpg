using Microsoft.Xna.Framework.Graphics;

public class LootUI
{
    public bool ShowLoot = true;

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!ShowLoot)
            return;

        foreach (DroppedItem item in Game1.World.Items)
        {
            // TODO: stacking loot plates
            item.Draw(spriteBatch);
        }
    }
}
