using System;
using Microsoft.Xna.Framework;

public enum Rarity
{
    Normal,
    Magic,
    Rare,
    Unique,
    Set,
}

public static class RarityExtensions
{
    public static Color GetColor(this Rarity rarity) =>
        rarity switch
        {
            Rarity.Normal => ItemColors.Normal,
            Rarity.Magic => ItemColors.Magic,
            Rarity.Rare => ItemColors.Rare,
            Rarity.Unique => ItemColors.Unique,
            Rarity.Set => ItemColors.Set,
            _ => Color.White,
        };
}
