using System;
using System.Collections.Generic;
using System.Linq;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Player : IActor
{
    public static readonly float ItemPickupRadius = 40;
    public static readonly float GoldPickupRadius = 60;

    public string Id { get; } = Guid.NewGuid().ToString();
    public ActorKind Kind { get; } = ActorKind.Player;
    public ActorState State { get; set; } = ActorState.Idling;
    public ActorActionState ActionState { get; set; } = ActorActionState.None;
    public ActorFacing Facing { get; set; } = ActorFacing.Right;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public bool IsAlive => Stats.Health > 0;
    public IHitbox Hitbox
    {
        get => new RectangleHitbox((int)Position.X - 12, (int)Position.Y - 24, 20, 50);
    }
    public Vector2 Size => new(140, 140);

    public SkillCollection Skills;
    public ActorBaseStats Stats { get; }
    public Equipment Equipment;
    public Inventory Inventory;
    public PlayerGold Gold;

    public PlayerInputComponent InputComponent;
    private PlayerGraphicsComponent _graphicsComponent;

    public Player()
    {
        Skills = new(this);
        Stats = new PlayerStats(
            this,
            speed: 100,
            health: 100,
            mana: 100,
            healthRegen: 0,
            manaRegen: 0
        );
        Equipment = new(this);
        Inventory = new(this);
        Gold = new();

        Equipment.Equip(new Sandals().ToMagic().Corrupted());
        Equipment.Equip(new Hood().ToRare());
        Equipment.Equip(new SapphireRing());
        Equipment.Equip(new RubyRing());

        for (int i = 0; i < 5; i++)
        {
            Inventory.AddItem(new OrbOfCorruption((i + 1) * 3));
        }

        // Inventory.AddItem(new Sandals().ToMagic().Corrupted());
        // Inventory.AddItem(new Sandals().ToMagic().Corrupted());
        // Inventory.AddItem(new Sandals().ToMagic().Corrupted());
        // Inventory.AddItem(new Sandals().ToMagic().Corrupted());
        // Inventory.AddItem(new Sandals().ToMagic().Corrupted());

        InputComponent = new(this);
        _graphicsComponent = new(this);
    }

    public void Update(GameTime gameTime)
    {
        Stats.Update(gameTime);

        var goldWithinRange = Game1
            .World.Items.Where(droppedItem => droppedItem.Item is Gold)
            .Where(item => item.Position.DistanceTo(Position) <= GoldPickupRadius)
            .ToList();

        foreach (var gold in goldWithinRange)
        {
            gold.GetPickedUp(this);
        }

        InputComponent.Update(gameTime);
        _graphicsComponent.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _graphicsComponent.Draw(spriteBatch);
    }

    public void TransitionState(ActorState newState)
    {
        bool stateChanged = State != newState;
        if (stateChanged)
        {
            State = newState;
            _graphicsComponent.ResetFrames();
        }
    }

    public void TakeDamage(double amount)
    {
        Stats.OffsetHealth(-amount);
    }

    public void OnKill(IMonster monster)
    {
        PlayerStats playerStats = (PlayerStats)Stats;
        playerStats.Level.GrantXP(monster.XP);
        playerStats.OffsetHealth(playerStats.HealthOnKill);
        playerStats.OffsetMana(playerStats.ManaOnKill);

        List<Item> loot = Game1.LootSystem.GenerateLoot(monster, this);
        foreach (var item in loot)
        {
            DroppedItem droppedItem = new(item, monster.Position);
            Game1.World.Items.Add(droppedItem);
        }
    }
}
