using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FrozenOrbGraphicsComponent
{
    private TextureAsset asset = Assets.Spells.FrozenOrb;

    public void Draw(FrozenOrbEntity frozenOrb, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            asset.Texture,
            new((int)frozenOrb.Position.X, (int)frozenOrb.Position.Y),
            asset.Frames[0],
            Color.White,
            frozenOrb.Rotation,
            new Vector2(asset.Texture.Width / asset.Frames.Count / 2, asset.Texture.Height / 2),
            1f,
            SpriteEffects.None,
            Layer.Entity
        );
    }
}
