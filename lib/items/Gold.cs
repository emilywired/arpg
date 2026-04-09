using System;
using System.Collections.Generic;
using arpg;
using Microsoft.Xna.Framework.Graphics;

public class Gold : Item
{
    public int Amount { get; }

    public Gold(int amount)
        : base($"{amount} Gold", Rarity.Normal, width: 0, height: 0, asset: null!)
    {
        Amount = amount;
    }

    public override bool GetPickedUp(Player player)
    {
        player.Gold.Add(Amount);
        return true;
    }
}
