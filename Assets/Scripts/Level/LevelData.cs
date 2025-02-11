using System;
using System.Collections.Generic;

[Serializable]
public class LevelData
{
    public int Number;
    public int MaxMoves;
    public List<LevelGoal> LevelGoals;
    public List<List<ItemType>> Tiles;  // Grid of tiles

    public int Width
    {
        get
        {
            return Tiles.Count;
        }
        set
        {
            if (value > Tiles.Count)
            {
                for (int j = Tiles.Count; j < value; j++)
                    Tiles.Add(new List<ItemType>(Tiles[0].Count));
            }
            else if (value < Tiles.Count)
            {
                while (Tiles.Count > value)
                    Tiles.RemoveAt(Tiles.Count - 1);
            }
        }
    }

    public int Height
    {
        get
        {
            return Tiles.Count > 0 ? Tiles[0].Count : 0;
        }
        set
        {
            for (int i = 0; i < Tiles.Count; i++)
            {
                var row = Tiles[i];

                if (value > row.Count)
                {
                    for (int j = row.Count; j < value; j++)
                        row.Add(0);
                }
                else if (value < row.Count)
                {
                    while (row.Count > value)
                        row.RemoveAt(row.Count - 1);
                }
            }
        }
    }

    public LevelData(int number, int width, int height)
    {
        Number = number;

        Tiles = new List<List<ItemType>>();
        for (int y = 0; y < height; y++)
        {
            var row = new List<ItemType>(width);
            for (int x = 0; x < width; x++)
            {
                row.Add(0);
            }
            Tiles.Add(row);
        }

        LevelGoals = new List<LevelGoal>();
    }
}
