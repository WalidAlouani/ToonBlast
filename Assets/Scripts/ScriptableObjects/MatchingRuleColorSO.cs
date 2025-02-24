using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MatchingRuleColor", menuName = "ScriptableObjects/CreateMatchingRule/Color", order = 1)]
public class MatchingRuleColorSO : MatchingRuleSO
{
    [SerializeField] private int minimumMatchCount = 2;
    [SerializeField]
    private List<Vector2Int> checkDirections = new List<Vector2Int>()
    {
        Vector2Int.down,
        Vector2Int.up,
        Vector2Int.left,
        Vector2Int.right,
    };

    private bool[,] visitedTiles;
    private List<TileItem> matchedTiles;

    public override List<TileItem> FindMatches(Board<TileItem> board, int x, int y)
    {
        visitedTiles = new bool[board.Width, board.Height];

        var clickedTile = board.GetElement(x, y);

        if (matchedTiles == null)
            matchedTiles = new List<TileItem>();
        else
            matchedTiles.Clear();

        matchedTiles.Add(clickedTile);
        visitedTiles[x, y] = true;

        CheckNeightbours(board, x, y);

        if (matchedTiles.Count < minimumMatchCount)
            matchedTiles.Clear();

        return matchedTiles;
    }

    private void CheckNeightbours(Board<TileItem> board, int x, int y)
    {
        for (int i = 0; i < checkDirections.Count; i++)
        {
            var checkDirection = checkDirections[i];
            var newX = x + checkDirection.x;
            var newY = y + checkDirection.y;

            if (!board.IsWithinBounds(newX, newY))
                continue;

            if (visitedTiles[newX, newY])
                continue;

            if (board[newX, newY] == null || board[x, y] == null)
                continue;

            if (board[newX, newY].ItemType != board[x, y].ItemType)
            {
                if (board[newX, newY].DamageType == TileDamageType.Proximity)
                {
                    visitedTiles[newX, newY] = true;
                    matchedTiles.Add(board[newX, newY]);
                }
                continue;
            }

            visitedTiles[newX, newY] = true;
            matchedTiles.Add(board[newX, newY]);
            CheckNeightbours(board, newX, newY);
        }
    }
}
