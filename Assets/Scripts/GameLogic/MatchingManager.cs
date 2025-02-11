using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingManager : MonoBehaviour
{
    // move to scriptable object
    [SerializeField] private int minimumMatchCount = 2;
    [SerializeField] private List<Vector2Int> checkDirections;

    private bool[,] visitedTiles;
    private List<TileItem> matchedTiles;
    private int width;
    private int height;

    public void Init(int width, int height)
    {
        this.width = width;
        this.height = height;
        visitedTiles = new bool[width, height];
        matchedTiles = new List<TileItem>();
    }

    public List<TileItem> FindMatches(TileItem[,] tiles, int x, int y)
    {
        ClearVisitedTiles();
        matchedTiles.Clear();
        
        matchedTiles.Add(tiles[x, y]);
        visitedTiles[x, y] = true;

        CheckNeightbours(tiles, x, y);

        if (matchedTiles.Count < minimumMatchCount)
            matchedTiles.Clear();

        return matchedTiles;
    }

    public void CheckNeightbours(TileItem[,] tiles, int x, int y)
    {
        for (int i = 0; i < checkDirections.Count; i++)
        {
            var checkDirection = checkDirections[i];
            var newX = x + checkDirection.x;
            var newY = y + checkDirection.y;

            if (newX < 0 || newX >= width || newY < 0 || newY >= height)
                continue;

            if (visitedTiles[newX, newY])
                continue;

            if (tiles[newX, newY] == null || tiles[x, y] == null || tiles[newX, newY].ItemType != tiles[x, y].ItemType)
                continue;

            visitedTiles[newX, newY] = true;
            matchedTiles.Add(tiles[newX, newY]);
            CheckNeightbours(tiles, newX, newY);
        }
    }

    private void ClearVisitedTiles()
    {
        for (int x = 0; x < visitedTiles.GetLength(0); x++)
        {
            for (int y = 0; y < visitedTiles.GetLength(1); y++)
            {
                visitedTiles[x, y] = false;
            }
        }
    }
}
