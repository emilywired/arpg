using System;
using System.Data;
using Microsoft.Xna.Framework;

public class Inventory
{
    public readonly int Width = 12;
    public readonly int Height = 5;
    public Grid<Item> Grid;

    private Player _player;

    public Inventory(Player player)
    {
        _player = player;
        Grid = new(Width, Height);
    }

    public bool AddItem(Item item)
    {
        bool added = Grid.AddItem(item, item.Width, item.Height);
        return added;
    }

    public bool AddItem(Item item, int x, int y)
    {
        bool added = Grid.AddItem(item, x, y, item.Width, item.Height);
        return added;
    }

    public Item? GetItem(int x, int y)
    {
        return Grid.GetItem(x, y);
    }

    public (int, int)? FindItemPosition(Item item)
        => Grid.FindItemPosition(item);

    public bool RemoveItem(Item item)
    {
        return Grid.RemoveItem(item);
    }
}
