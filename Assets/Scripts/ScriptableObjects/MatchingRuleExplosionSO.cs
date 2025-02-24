using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MatchingRuleExplosion", menuName = "ScriptableObjects/CreateMatchingRule/Explosion", order = 2)]
public class MatchingRuleExplosionSO : MatchingRuleSO
{
    [SerializeField] private int range = 2;

    public override List<TileItem> FindMatches(Board<TileItem> board, int x, int y)
    {
        var matchedTiles = new List<TileItem>();

        int startX = Mathf.Max(0, x - range); // Ensure startX is not less than 0
        int endX = Mathf.Min(board.Width - 1, x + range); // Ensure endX is not greater than width - 1
        int startY = Mathf.Max(0, y - range); // Ensure startY is not less than 0
        int endY = Mathf.Min(board.Height - 1, y + range); // Ensure endY is not greater than height - 1

        for (int currentX = startX; currentX <= endX; currentX++)
        {
            for (int currentY = startY; currentY <= endY; currentY++)
            {
                var tile = board[currentX, currentY];
                if (tile == null)
                    continue;

                matchedTiles.Add(tile);
            }
        }

        return matchedTiles;
    }
}
