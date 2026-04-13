using Microsoft.Xna.Framework;

public class TestAnimation : Entity
{
    public float Rotation { get; set; }

    public override IHitbox Hitbox
        => new RectangleHitbox((int)Position.X, (int)Position.Y, 64, 64);

    private ITransform transform;

    private Sprite sprite;

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

        AddDrawable(sprite = new Sprite(Assets.RectangleTexture));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        transform.Update(gameTime);

        sprite.Position = Position;
        sprite.Rotation = Rotation;
    }
}