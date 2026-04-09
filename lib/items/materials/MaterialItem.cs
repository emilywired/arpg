using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

public class MaterialItem : Item, IMaterial
{
    public int StackQuantity { get; set; }
    public int MaxStackQuantity { get; }
    public string Description { get; }

    public MaterialItem(
        string name,
        Rarity rarity,
        int width,
        int height,
        TextureAsset asset,
        string description,
        int maxStackQuantity,
        int stackQuantity = 1
    )
        : base(name, rarity, width, height, asset)
    {
        StackQuantity = stackQuantity;
        MaxStackQuantity = maxStackQuantity;
        Description = description;
    }
}
