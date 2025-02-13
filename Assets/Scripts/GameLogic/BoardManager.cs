using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private ItemInventorySO itemsInventory;
    [SerializeField] private Transform boardContainer;
    [SerializeField] private TileItem tilePrefab;

    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private MatchingManager matchingManager;
    [SerializeField] private MovesManager movesManager;
    [SerializeField] private GoalManager goalManager;
    [SerializeField] private int fallOffset = 10;

    private TileItem[,] boardTiles;
    private ItemType[] activeTypes;
    private ObjectPool<TileItem> tilePool;
    private bool stopInputs = false;

    public Action<int, int> OnTileTapped;
    public Action<Vector2Int, List<TileItem>> OnTilesDestroyed;


    private void Awake()
    {
        tilePool = new ObjectPool<TileItem>(tilePrefab, boardContainer);
    }

    private void Start()
    {
        gameStateManager.OnStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        gameStateManager.OnStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                inputHandler.OnTileClicked += OnTileClicked;
                break;
            case GameState.Paused:
            case GameState.LevelCompleted:
            case GameState.GameOver:
                inputHandler.OnTileClicked -= OnTileClicked;
                break;
        }
    }

    public void Init(int width, int height, List<List<ItemType>> tileData, List<ItemType> activeTypes)
    {
        this.activeTypes = activeTypes.ToArray();
        boardTiles = new TileItem[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                boardTiles[x, y] = CreateTile(x, y, tileData[x][y]);
            }
        }
    }

    public TileItem CreateTile(int x, int y, ItemType tileType)
    {
        var tileItem = tilePool.Get();

        tileItem.OnDestroy += OnTileDestroyed;
        // Get tile type from tileData parameter
        if (tileType == ItemType.None)
            tileType = ArrayUtils.GetRandomValue(activeTypes);

        var typeData = itemsInventory.GetItemByType(tileType);
        tileItem.Init(x, y, typeData);
        return tileItem;
    }

    private void OnTileClicked(GridElement element)
    {
        if (stopInputs) return;

        var matches = matchingManager.FindMatches(boardTiles, element.X, element.Y);

        if (matches.Count <= 0)
            return;

        foreach (var tile in matches)
            tile.Destroy();

        OnTilesDestroyed?.Invoke(new Vector2Int(element.X, element.Y), matches);

        goalManager.UpdateGoal(matches);
        movesManager.DecreaseMoves();

        StartCoroutine(RearrangeAndRefill(0.2f));
    }

    private IEnumerator RearrangeAndRefill(float delay)
    {
        stopInputs = true;
        yield return new WaitForSeconds(delay);
        RearrangeBoard();
        RefillBoard();
        stopInputs = false;
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
                boardTiles[x, y] = CreateTile(x, y + fallOffset, ItemType.None);
                boardTiles[x, y].UpdateCoordinates(x, y);
            }
        }
    }

    private void OnTileDestroyed(TileItem tileItem)
    {
        tileItem.OnDestroy -= OnTileDestroyed;
        if (boardTiles[tileItem.X, tileItem.Y] == tileItem)
            boardTiles[tileItem.X, tileItem.Y] = null;
        tilePool.ReturnToPool(tileItem);
    }
}