using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using arpg;
using Microsoft.Xna.Framework;

public class World
{
    public readonly Player Player;
    public readonly List<DroppedItem> Items = [];
    public readonly List<IEntity> Entities = [];
    public readonly List<IActor> Actors = [];
    private MonsterSpawner _monsterSpawner;

    public World(Player player)
    {
        Player = player;
        _monsterSpawner = new MonsterSpawner(Player, 0.5d, offscreenDistance: 100);

        // Items.Add(new DroppedItem(new AugmentingCore(), new(0, 0)));
        // Items.Add(new DroppedItem(new Hood(), new(50, 0)));
        // Items.Add(new DroppedItem(new Sandals(), new(0, 50)));
        // Items.Add(new DroppedItem(new SapphireRing().ToRare(), new(0, 100)));
        // Items.Add(new DroppedItem(new RubyRing(), new(0, 200)));

        Actors.Add(Player);
    }

    public void Update(GameTime gameTime)
    {
        _monsterSpawner.Update(gameTime);

        for (int i = Actors.Count - 1; i >= 0; i--)
        {
            IActor actor = Actors[i];
            actor.Update(gameTime);
        }

        for (int i = Entities.Count - 1; i >= 0; i--)
        {
            IEntity entity = Entities[i];
            entity.Update(gameTime);
        }

        for (int i = Items.Count - 1; i >= 0; i--)
        {
            DroppedItem item = Items[i];
            item.Update(gameTime);
        }
    }

    public void RemoveEntity(IEntity entity)
    {
        int index = Entities.FindIndex(e => e.Id == entity.Id);
        Entities.RemoveAt(index);
    }

    public void RemoveActor(IActor actor)
    {
        int index = Actors.FindIndex(e => e.Id == actor.Id);
        Actors.RemoveAt(index);
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
