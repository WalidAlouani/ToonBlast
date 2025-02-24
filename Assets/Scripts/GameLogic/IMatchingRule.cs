using System.Collections.Generic;

public interface IMatchingRule
{
    List<TileItem> FindMatches(Board<TileItem> board, int startX, int startY);
}