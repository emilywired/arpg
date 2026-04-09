using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FrozenOrbEntity : IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Vector2 Position { get; set; }
    public float Speed { get; set; } = 100f;
    public readonly float MaxDuration = 3f;
    public double Angle = 0d;
    public float Rotation = 0f;
    public IHitbox Hitbox
    {
        get => new RectangleHitbox(0, 0, 0, 0);
    }

    private IActor _owner;
    private FrozenOrbGraphicsComponent _frozenOrbGraphicsComponent = new();
    private FrozenOrbBehaviorComponent _frozenOrbBehaviorComponent = new();

    public FrozenOrbEntity(IActor owner)
    {
        _owner = owner;
        Game1.World.Entities.Add(this);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _frozenOrbGraphicsComponent.Draw(this, spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        Rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * 3f;
        _frozenOrbBehaviorComponent.Update(this, gameTime);
    }

    public void Destroy()
    {
        Game1.World.RemoveEntity(this);
    }
}
