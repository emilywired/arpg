using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public class Inventory
{
    public readonly int Width = 12;
    public readonly int Height = 5;
    public Grid<Item> Grid;

    public Inventory()
    {
        Grid = new(Width, Height);
    }

    /// <summary>
    /// Adds item to inventory, stacks to existing items if able.
    /// </summary>
    /// <returns>
    /// If item should be deleted after.
    /// </returns>
    public bool AddItem(Item item)
    {
        if (item is MaterialItem materialItem)
        {
            StackMaterial(materialItem);
            if (materialItem.StackQuantity == 0)
                return true;
        }

        bool added = Grid.AddItem(item, item.Width, item.Height);
        return added;
    }

    private void StackMaterial(MaterialItem item)
    {
        IEnumerable<MaterialItem> existingMaterials = Grid.Items()
            .Where(material => material.Name == item.Name)
            .Cast<MaterialItem>();

        foreach (MaterialItem existingMaterial in existingMaterials)
        {
            int remainingCapacity =
                existingMaterial.MaxStackQuantity - existingMaterial.StackQuantity;

            int canAdd = Math.Min(item.StackQuantity, remainingCapacity);

            existingMaterial.StackQuantity += canAdd;
            item.StackQuantity -= canAdd;

            if (remainingCapacity == 0)
                break;
        }
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

    public (int, int)? FindItemPosition(Item item) => Grid.FindItemPosition(item);

    public bool RemoveItem(Item item)
    {
        return Grid.RemoveItem(item);
    }
}
