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

    public ReactiveProperty<ActorState> State { get; } = new(ActorState.Idling);
    ActorState IActor.State => State.Value;

    public ActorActionState ActionState { get; set; } = ActorActionState.None;
    public ActorFacing Facing { get; set; } = ActorFacing.Right;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public bool IsAlive => Stats.Health.Value > 0;
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
    private PlayerSprite sprite;

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

        InputComponent = new(this);
        sprite = new(this);
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

        sprite.Position = Position;
        sprite.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch);
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
