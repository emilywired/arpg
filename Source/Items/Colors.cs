using Microsoft.Xna.Framework;

public static class ItemColors
{
    public static Color Normal => Color.White; // rgb(255, 255, 255)
    public static Color Magic => new(54, 91, 214); // rgb(54, 91, 214)
    public static Color Rare => new(241, 210, 30); // rgb(241, 210, 30)
    public static Color Unique => new(207, 113, 6); // rgb(207, 113, 6)
    public static Color Set => new(112, 194, 18); // rgb(112, 194, 18)
}

public static class Colors
{
    public static Color Health => new(150, 10, 10);
    public static Color Mana => new(10, 10, 140);
}
