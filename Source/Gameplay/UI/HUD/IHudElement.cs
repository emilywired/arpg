using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IHudElement
{
    void Draw(SpriteBatch spriteBatch);
    void Update(GameTime gameTime);
};
