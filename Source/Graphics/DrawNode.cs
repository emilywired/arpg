using Microsoft.Xna.Framework.Graphics;

public abstract class DrawNode(IDrawable source)
{
    public IDrawable Source { get; } = source;

    public abstract void Draw(SpriteBatch spriteBatch);
}

public abstract class DrawNode<T>(T source) : DrawNode(source)
    where T : IDrawable
{
    public new T Source => (T)base.Source;
}