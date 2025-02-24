using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MatchingRuleLine", menuName = "ScriptableObjects/CreateMatchingRule/Line", order = 3)]
public class MatchingRuleLineSO : MatchingRuleSO
{
    [SerializeField] private LineDirection direction;
    [SerializeField] private int lineCount = 1;

    enum LineDirection { Horizontal, Vecrtical, Both }

    public override List<TileItem> FindMatches(Board<TileItem> board, int x, int y)
    {
        var matchedTiles = new List<TileItem>();

        int half = lineCount / 2;

        switch (direction)
        {
            case LineDirection.Horizontal:
                for (int row = y - half; row <= y + half; row++)
                {
                    if (row < 0 || row >= board.Height)
                        continue;

                    for (int col = 0; col < board.Width; col++)
                    {
                        TileItem tile = board.GetElement(col, row);
                        if (tile != null)
                        {
                            matchedTiles.Add(tile);
                        }
                    }
                }
                break;

            case LineDirection.Vecrtical:
                for (int col = x - half; col <= x + half; col++)
                {
                    if (col < 0 || col >= board.Width)
                        continue;

                    for (int row = 0; row < board.Height; row++)
                    {
                        TileItem tile = board.GetElement(col, row);
                        if (tile != null)
                        {
                            matchedTiles.Add(tile);
                        }
                    }
                }
                break;

            case LineDirection.Both:
                for (int row = y - half; row <= y + half; row++)
                {
                    if (row < 0 || row >= board.Height)
                        continue;

                    for (int col = 0; col < board.Width; col++)
                    {
                        TileItem tile = board.GetElement(col, row);
                        if (tile != null)
                        {
                            matchedTiles.Add(tile);
                        }
                    }
                }

                for (int col = x - half; col <= x + half; col++)
                {
                    if (col < 0 || col >= board.Width)
                        continue;

                    for (int row = 0; row < board.Height; row++)
                    {
                        TileItem tile = board.GetElement(col, row);
                        if (tile != null && !matchedTiles.Contains(tile))
                        {
                            matchedTiles.Add(tile);
                        }
                    }
                }

                break;
        }

        return matchedTiles;
    }
}
