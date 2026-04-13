using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

public class CompositeDrawNode(IDrawable source, IEnumerable<DrawNode> childDrawables) : DrawNode(source)
{
    public override void Draw(SpriteBatch spriteBatch)
    {
        foreach (DrawNode drawable in childDrawables)
        {
            drawable.Draw(spriteBatch);
        }
    }
}