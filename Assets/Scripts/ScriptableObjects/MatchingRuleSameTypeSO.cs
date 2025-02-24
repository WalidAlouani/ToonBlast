using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MatchingRuleSameType", menuName = "ScriptableObjects/CreateMatchingRule/SameType", order = 4)]
public class MatchingRuleSameTypeSO : MatchingRuleSO
{
    public override List<TileItem> FindMatches(Board<TileItem> board, int x, int y)
    {
        var clickedTile = board.GetElement(x, y);
        var clickedTileType = clickedTile.ItemType;

        var matchedTiles = new List<TileItem>();

        for (int row = 0; row < board.Width; row++)
        {
            for (int column = 0; column < board.Height; column++)
            {
                var tile = board[row, column];
                if (tile == null || tile.ItemType != clickedTileType)
                    continue;

                matchedTiles.Add(tile);
            }
        }

        return matchedTiles;
    }
}
