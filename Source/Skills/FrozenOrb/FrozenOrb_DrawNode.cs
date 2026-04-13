using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FrozenOrbDrawNode(FrozenOrbEntity source) : DrawNode<FrozenOrbEntity>(source)
{
    private TextureAsset asset = Assets.Spells.FrozenOrb;

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            asset.Texture,
            new((int)Source.Position.X, (int)Source.Position.Y),
            asset.Frames[0],
            Color.White,
            Source.Rotation,
            new Vector2(asset.Texture.Width / asset.Frames.Count / 2, asset.Texture.Height / 2),
            1f,
            SpriteEffects.None,
            Layer.Entity
        );
    }
}
