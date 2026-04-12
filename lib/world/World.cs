using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

public class World
{
    public Player Player { get; }
    public List<DroppedItem> Items { get; } = [];

    private readonly List<Entity> entities = [];
    private readonly List<Entity> entityQueue = [];
    public IEnumerable<Entity> Entities => entities;

    public Stash Stash { get; } = new(new(30, 30));

    private MonsterSpawner _monsterSpawner;

    public World(Player player)
    {
        Player = player;
        _monsterSpawner = new MonsterSpawner(Player, 0.75d, offscreenDistance: 80);
        entities.Add(Player);
    }

    public void Update(GameTime gameTime)
    {
        entities.AddRange(entityQueue);
        entityQueue.Clear();

        _monsterSpawner.Update(gameTime);

        Stash.Update(gameTime);

        foreach (var entity in entities)
        {
            entity.Update(gameTime);
        }

        for (int i = Items.Count - 1; i >= 0; i--)
        {
            DroppedItem item = Items[i];
            item.Update(gameTime);
        }

        var entitiesToRemove = entities.Where(e => e.IsDestroyed).ToList();
        foreach (var entity in entitiesToRemove)
        {
            entities.Remove(entity);
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
            var itemInRange = hoveredItems.Find(item =>
                Player.Position.DistanceTo(item.Position) <= Player.ItemPickupRadius
            );
            if (itemInRange != null)
            {
                itemInRange.GetPickedUp(Player); // TODO: player.PickUpItem(item)?
            }
            else
            {
                var targetItem = hoveredItems.First();

                Vector2 pickupPoint = MathUtils.ClosestEdgeOfCircle(
                    targetItem.Position,
                    Player.ItemPickupRadius - 1,
                    Player.Position
                );

                var track = Player.InputComponent.StartMove(pickupPoint);
                track.OnComplete += () =>
                {
                    if (
                        Items.Contains(targetItem)
                        && Player.Position.DistanceTo(targetItem.Position)
                            <= Player.ItemPickupRadius
                    )
                    {
                        targetItem.GetPickedUp(Player);
                    }
                };
            }

            return true;
        }

        return false;
    }
}
