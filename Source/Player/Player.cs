using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Player : Actor
{
    public static readonly float ItemPickupRadius = 40;
    public static readonly float GoldPickupRadius = 60;

    public override IHitbox Hitbox =>
        new RectangleHitbox((int)Position.X - 12, (int)Position.Y - 24, 20, 50);

    public Vector2 Size => new(140, 140);

    public SkillCollection Skills;
    public override PlayerStats Stats { get; }

    public Equipment Equipment;
    public Inventory Inventory;
    public PlayerGold Gold;

    public PlayerInputComponent InputComponent;
    private AnimatedSprite sprite;

    private TextureAsset idleAsset = Assets.Player.Idle;
    private TextureAsset walkAsset = Assets.Player.Walk;

    public Player()
    {
        Skills = new(this);
        Stats = new PlayerStats(speed: 100, health: 100, mana: 100, healthRegen: 0, manaRegen: 0);
        Equipment = new(this);
        Inventory = new();
        Gold = new();

        Equipment.Equip(new Sandals().ToMagic().Corrupted());
        Equipment.Equip(new Hood().ToRare());
        Equipment.Equip(new SapphireRing());
        Equipment.Equip(new RubyRing());

        InputComponent = new(this);
        AddDrawable(sprite = new());

        State.Connect(
            this,
            () =>
            {
                sprite.SetTextureAsset(
                    State.Value switch
                    {
                        ActorState.Idling => idleAsset,
                        ActorState.Walking => walkAsset,
                        _ => throw new Exception("Unhandled ActorState"),
                    }
                );
            }
        );
    }

    public override void Update(float dt)
    {
        base.Update(dt);
        Stats.Update(dt);

        var goldWithinRange = Game1
            .World.Items.Where(droppedItem => droppedItem.Item is Gold)
            .Where(item => item.Position.DistanceTo(Position) <= GoldPickupRadius)
            .ToList();

        foreach (DroppedItem? gold in goldWithinRange)
        {
            _ = gold.GetPickedUp(this);
        }

        InputComponent.Update(dt);

        sprite.SpriteEffects =
            Facing == ActorFacing.Right ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
    }

    public void OnKill(Monster monster)
    {
        Stats.Level.GrantXP(monster.XP);
        Stats.OffsetHealth(Stats.HealthOnKill);
        Stats.OffsetMana(Stats.ManaOnKill);

        List<Item> loot = Game1.LootSystem.GenerateLoot(monster, this);
        foreach (Item item in loot)
        {
            DroppedItem droppedItem = new(item, monster.Position);
            Game1.World.Items.Add(droppedItem);
        }
    }
}
