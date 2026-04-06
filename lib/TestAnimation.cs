using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class TestAnimation : IEntity
{
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public string Id { get; set; } = "abc";

    public IHitbox Hitbox => new RectangleHitbox((int)Position.X, (int)Position.Y, 64, 64);

    private ITransform transform;

    public TestAnimation()
    {
        transform = new TransformSequence([
            new TransformList([
                new TransformVector2(this, nameof(Position), new(100, 100), 1),
                new TransformFloat(this, nameof(Rotation), 3.14f, 1),
            ]),
            new TransformList([
                new TransformVector2(this, nameof(Position), Vector2.Zero, 1),
                new TransformFloat(this, nameof(Rotation), 0f, 1),
            ])
        ]);
        transform.OnFinish += Destroy;
        transform.Reset();
    }

    public void Destroy()
    {
        Game1.World.Entities.Remove(this);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Assets.RectangleTexture,
            new(Position.ToPoint(), new(64, 64)),
            null,
            Color.Yellow,
            Rotation,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.Hitbox
        );
    }

    public void Update(GameTime gameTime)
    {
        transform.Update(gameTime);
    }
}