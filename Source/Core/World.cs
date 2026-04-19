using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class World
{
    public Player Player { get; }
    public List<DroppedItem> Items { get; } = [];

    private readonly List<Entity> entities = [];
    private readonly List<Entity> entityQueue = [];
    public IEnumerable<Entity> Entities => entities;

    public Stash Stash { get; } = new(new(30, 30));
    private LootPlates lootPlates = new();

    private MonsterSpawner monsterSpawner;

    public World(Player player)
    {
        Player = player;
        monsterSpawner = new MonsterSpawner(Player, 0.75d, offscreenDistance: 80);
        entities.Add(Player);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        lootPlates.Draw(spriteBatch);

        foreach (Entity entity in Entities)
        {
            if (!entity.Hidden)
            {
                entity.Draw(spriteBatch);
            }
        }

        Stash.Draw(spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        entities.AddRange(entityQueue);
        entityQueue.Clear();

        monsterSpawner.Update(gameTime);

        Stash.Update(gameTime);

        foreach (Entity entity in entities)
        {
            entity.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        for (int i = Items.Count - 1; i >= 0; i--)
        {
            DroppedItem item = Items[i];
            item.Update(gameTime);
        }

        var entitiesToRemove = entities.Where(e => e.IsDestroyed).ToList();
        foreach (Entity? entity in entitiesToRemove)
        {
            _ = entities.Remove(entity);
        }
    }

    public void RemoveEntity(Entity entity)
    {
        if (!entity.IsDestroyed)
            entity.Destroy();
    }

    public void AddEntity(Entity entity)
    {
        entityQueue.Add(entity);
    }

    public bool OnLeftClick()
    {
        var hoveredItems = Items.Where(item => item.IsHovered).ToList();

        if (hoveredItems.Count != 0)
        {
            DroppedItem? itemInRange = hoveredItems.Find(item =>
                Player.Position.DistanceTo(item.Position) <= Player.ItemPickupRadius
            );
            if (itemInRange != null)
            {
                _ = itemInRange.GetPickedUp(Player);
            }
            else
            {
                DroppedItem targetItem = hoveredItems.First();

                Vector2 pickupPoint = MathUtils.ClosestEdgeOfCircle(
                    targetItem.Position,
                    Player.ItemPickupRadius - 1,
                    Player.Position
                );

                PlayerInputComponent.MovementTrack track = Player.InputComponent.StartMove(
                    pickupPoint
                );
                track.OnComplete += () =>
                {
                    if (
                        Items.Contains(targetItem)
                        && Player.Position.DistanceTo(targetItem.Position)
                            <= Player.ItemPickupRadius
                    )
                    {
                        _ = targetItem.GetPickedUp(Player);
                    }
                };
            }

            return true;
        }

        return false;
    }
}
