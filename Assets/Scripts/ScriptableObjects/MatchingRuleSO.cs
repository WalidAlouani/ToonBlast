using System.Collections.Generic;
using UnityEngine;


public abstract class MatchingRuleSO : ScriptableObject, IMatchingRule
{
    public abstract List<TileItem> FindMatches(Board<TileItem> board, int startX, int startY);
}
