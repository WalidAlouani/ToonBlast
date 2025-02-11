using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private ItemInventorySO itemsInventory;
    [SerializeField] private Transform boardContainer;
    [SerializeField] private TileItem tilePrefab;

    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private MatchingManager matchingManager;
    [SerializeField] private MovesManager movesManager;
    [SerializeField] private GoalManager goalManager;

    private TileItem[,] boardTiles;

    public Action<int, int> OnTileTapped;

    private void Start()
    {
        inputHandler.OnTileClicked += OnTileClicked;
    }

    public void Init(int width, int height, List<List<ItemType>> tileData)
    {
        boardTiles = new TileItem[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                boardTiles[x, y] = CreateTile(x, y, tileData[x][y]);
            }
        }
    }

    private TileItem CreateTile(int x, int y, ItemType tileType)
    {
        TileItem tileItem = Instantiate(tilePrefab, boardContainer);
        // Get tile type from tileData parameter
        if (tileType == ItemType.None)
            tileType = EnumUtils.GetRandomValue<ItemType>();

        var typeData = itemsInventory.GetItemByType(tileType);
        tileItem.Init(x, y, typeData);
        return tileItem;
    }

    private void OnTileClicked(GridElement element)
    {
        List<TileItem> matches = matchingManager.FindMatches(boardTiles, element.X, element.Y);

        if (matches.Count <= 0)
            return;

        // Trigger any tile-specific logic
        foreach (var match in matches)
        {
            match.Tapped();
        }

        movesManager.DecreaseMoves();
        goalManager.UpdateGoal(matches);

        RearrangeBoard();
        RefillBoard();
    }

    private void RearrangeBoard()
    {
        int width = boardTiles.GetLength(0);
        int height = boardTiles.GetLength(1);

        // For each column starting from y=1 (skip bottom row)
        for (int y = 1; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (boardTiles[x, y] == null)
                    continue;

                int targetY = y;
                // Find the lowest available slot in the column
                while (targetY > 0 && boardTiles[x, targetY - 1] == null)
                    targetY--;

                if (targetY == y)
                    continue;

                // Swap the tile to the new position
                (boardTiles[x, targetY], boardTiles[x, y]) = (boardTiles[x, y], boardTiles[x, targetY]);
                boardTiles[x, targetY].UpdateCoordinates(x, targetY);
            }
        }
    }

    private void RefillBoard()
    {
        int width = boardTiles.GetLength(0);
        int height = boardTiles.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (boardTiles[x, y] != null)
                    continue;

                // Create a random new tile with a falling effect (offset position)
                boardTiles[x, y] = CreateTile(x, y + 5, ItemType.None);
                boardTiles[x, y].UpdateCoordinates(x, y);
            }
        }
    }
}