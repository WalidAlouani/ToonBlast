using System;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private ItemInventorySO itemsInventory;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Transform boardContainer;
    [SerializeField] private TileItem tilePrefab;

    [SerializeField] private GridManager gridManager;
    [SerializeField] private MatchingManager matchingManager;
    [SerializeField] private MovesManager movesManager;
    [SerializeField] private GoalManager goalManager;

    private TileItem[,] tiles;

    public Action<int, int> OnTileTapped;

    private void Start()
    {
        inputHandler.OnTileClicked += OnTileClicked;
    }

    public void Init(int width, int height, List<List<ItemType>> tiles)
    {
        this.tiles = new TileItem[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var position = gridManager.GetGridPosition(x, y);

                var tileItem = Instantiate(tilePrefab, position, Quaternion.identity, boardContainer);
                var tile = tiles[x][y];

                if (tile == ItemType.None) tile = EnumUtils.GetRandomValue<ItemType>();

                var typeData = itemsInventory.GetItemByType(tile);

                tileItem.Init(x, y, typeData);
                this.tiles[x, y] = tileItem;
            }
        }
    }

    private void OnTileClicked(GridElement element)
    {
        var matches = matchingManager.FindMatches(tiles, element.X, element.Y);

        if (matches.Count <= 0)
            return;

        foreach (var match in matches)
            match.Tapped();

        movesManager.DecreaseMoves();
        goalManager.UpdateGoal(matches);

        Rearrange();
        Refill();
    }

    private void Rearrange()
    {
        for (int y = 1; y < tiles.GetLength(1); y++)
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                if (tiles[x, y] == null)
                    continue;

                int lowestY = y;
                while (lowestY > 0 && tiles[x, lowestY - 1] == null)
                    lowestY--;

                if (lowestY == y) // No change needed
                    continue;

                (tiles[x, lowestY], tiles[x, y]) = (tiles[x, y], tiles[x, lowestY]);
                tiles[x, lowestY].UpdatePosition(gridManager.GetGridPosition(x, lowestY));
                tiles[x, lowestY].UpdateCoordinates(x, lowestY);
            }
        }
    }

    private void Refill()
    {
        for (int y = 0; y < tiles.GetLength(1); y++)
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                if (tiles[x, y] != null)
                    continue;

                var position = gridManager.GetGridPosition(x, y);

                var tileItem = Instantiate(tilePrefab, position + Vector3.up * 5, Quaternion.identity, boardContainer);
                var tile = EnumUtils.GetRandomValue<ItemType>();
                var typeData = itemsInventory.GetItemByType(tile);

                tileItem.Init(x, y, typeData);
                this.tiles[x, y] = tileItem;
                tileItem.UpdatePosition(position);
            }
        }
    }
}
