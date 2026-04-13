using System.Collections.Generic;

public class GridItem<T>
{
    public required T Value;
    public bool IsOriginSquare = false;
    public required int OriginX;
    public required int OriginY;
    public required int Width;
    public required int Height;
}

public class Grid<T>
    where T : class
{
    public int Width;
    public int Height;

    private GridItem<T>?[,] Squares;

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
        Squares = new GridItem<T>?[Height, Width];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Squares[y, x] = null;
            }
        }
    }

    public bool AddItem(T item, int width, int height)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                bool itemFits = ItemFits(x, y, width, height);
                bool added = AddItem(item, x, y, width, height);
                if (added)
                    return true;
            }
        }
        return false;
    }

    public bool AddItem(T item, int originX, int originY, int width, int height)
    {
        if (!ItemFits(originX, originY, width, height))
            return false;

        for (int x = originX; x < originX + width; x++)
        {
            for (int y = originY; y < originY + height; y++)
            {
                Squares[y, x] = new()
                {
                    Value = item,
                    OriginX = originX,
                    OriginY = originY,
                    Width = width,
                    Height = height,
                };
            }
        }

        Squares[originY, originX]!.IsOriginSquare = true;

        return true;
    }

    public GridItem<T>? GetGridItem(int x, int y)
        => Squares[y, x];

    public T? GetItem(int x, int y)
        => Squares[y, x]?.Value;

    public IEnumerable<T> Items()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                GridItem<T>? item = Squares[y, x];
                if (item != null)
                    yield return item.Value;
            }
        }
    }

    public bool RemoveItem(T item)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                GridItem<T>? gridItem = Squares[y, x];
                if (gridItem?.Value == item)
                {
                    RemoveGridItem(gridItem);
                    return true;
                }
            }
        }

        return false;
    }

    public void RemoveGridItem(GridItem<T> gridItem)
    {
        for (int dx = 0; dx < gridItem.Width; dx++)
        {
            for (int dy = 0; dy < gridItem.Height; dy++)
            {
                Squares[gridItem.OriginY + dy, gridItem.OriginX + dx] = null;
            }
        }
    }

    public bool SquareIsOriginSquare(int x, int y)
    {
        return Squares[y, x]?.IsOriginSquare ?? false;
    }

    public bool SquareExists(int x, int y)
    {
        return (x >= 0 && x < Width && y >= 0 && y < Height);
    }

    public bool SquareIsTaken(int x, int y)
    {
        return Squares[y, x] != null;
    }

    public bool ItemFits(int originX, int originY, int width, int height)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int x = originX + i;
                int y = originY + j;
                if (!SquareExists(x, y) || SquareIsTaken(x, y))
                    return false;
            }
        }

        return true;
    }

    public (int, int)? FindItemPosition(T item)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                GridItem<T>? gridItem = Squares[y, x];
                if (gridItem?.Value == item)
                    return (gridItem.OriginX, gridItem.OriginY);
            }
        }

        return null;
    }

    // public bool ItemFitsWithinGrid(int width, int height)
    // {
    //     // TODO: optimize
    //     for (int x = 0; x < Width; x++)
    //     {
    //         for (int y = 0; y < Height; y++)
    //         {
    //             bool itemFits = ItemFits(x, y, width, height);
    //             if (itemFits)
    //                 return true;
    //         }
    //     }

    //     return false;
    // }
}
