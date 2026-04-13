using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FrozenOrbGraphicsComponent
{
    private TextureAsset _asset = Assets.Spells.FrozenOrb;

    public void Draw(FrozenOrbEntity frozenOrb, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            _asset.Texture,
            new((int)frozenOrb.Position.X, (int)frozenOrb.Position.Y),
            _asset.Frames[0],
            Color.White,
            frozenOrb.Rotation,
            new Vector2(_asset.Texture.Width / _asset.Frames.Count / 2, _asset.Texture.Height / 2),
            1f,
            SpriteEffects.None,
            Layer.Entity
        );
    }
}
