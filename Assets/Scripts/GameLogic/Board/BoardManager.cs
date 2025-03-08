using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private TileFactory tileFactory;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private BoardAnimationController animationController;
    [SerializeField] private int fallOffset = 10;

    private TileBoard board;

    // Add BoardStateManager
    private bool blockInputs = false;

    private void OnEnable()
    {
        ServiceLocator.Get<EventManager>().OnGameStateChanged.Subscribe(OnGameStateChanged, this);
    }

    private void OnDisable()
    {
        if (ServiceLocator.TryGet<EventManager>(out var eventManager))
            eventManager.OnGameStateChanged.Unsubscribe(OnGameStateChanged);
    }

    public void Init(int width, int height, List<List<ItemType>> tileData)
    {
        board = new TileBoard(width, height);

        board.ForEach((x, y) =>
        {
            CreateTile(x, y, tileData[x][y]);
        });
    }

    private void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                inputHandler.OnGridClicked += OnGridClicked;
                break;
            case GameState.Paused:
            case GameState.LevelCompleted:
            case GameState.GameOver:
                inputHandler.OnGridClicked -= OnGridClicked;
                break;
        }
    }

    private TileItem CreateTile(int x, int y, ItemType itemType = ItemType.None)
    {
        var tileItem = tileFactory.CreateTile(x, y, itemType);
        board.SetElement(tileItem);

        tileItem.OnClick += OnTileClicked;
        tileItem.OnDestroy += OnTileDestroyed;

        return tileItem;
    }

    private void OnGridClicked(Vector2 position)
    {
        if (blockInputs) return;

        var clickedTile = board.GetElementFromWorldPosition(position);

        clickedTile?.Click();
    }

    private void OnTileClicked(TileItem clickedTile)
    {
        var matchingRule = clickedTile.MatchingRule;
        if (matchingRule == null)
            return;

        var matches = clickedTile.MatchingRule.FindMatches(board, clickedTile.X, clickedTile.Y);

        if (matches.Count <= 0)
            return;

        var destroyed = new List<TileItem>();

        foreach (var tile in matches)
        {
            if (tile.Damage())
                destroyed.Add(tile);
        }

        ServiceLocator.Get<EventManager>().OnTilesDestroyed.Publish(destroyed);

        StartCoroutine(RearrangeAndRefill());
    }

    private void OnTileDestroyed(TileItem tileItem)
    {
        tileItem.OnClick -= OnTileClicked;
        tileItem.OnDestroy -= OnTileDestroyed;

        //if (!board.IsEqualTo(tileItem.X, tileItem.Y, tileItem))
        //    return;

        board.RemoveElement(tileItem.X, tileItem.Y);
    }

    private IEnumerator RearrangeAndRefill()
    {
        blockInputs = true;
        RearrangeBoard();
        RefillBoard();
        yield return new WaitForSeconds(animationController.RefillAndFallDuration);
        blockInputs = false;
    }

    private void RearrangeBoard()
    {
        board.ForEach((x, y) =>
        {
            if (board.ElementIsNull(x, y) || !board.GetElement(x, y).CanFall)
                return;

            int targetY = y;
            // Find the lowest available slot in the column
            while (targetY > 0 && board.ElementIsNull(x, targetY - 1))
                targetY--;

            if (targetY == y)
                return;

            // Swap the tile to the new position
            board.SwapElements(x, targetY, x, y);
            
            var tileItem = board.GetElement(x, targetY);

            // Tile rearrange animation
            animationController.RearrangeAnimation(tileItem, new Vector2Int(x, targetY));
        });
    }

    private void RefillBoard()
    {
        board.ForEach((x, y) =>
        {
            if (!board.ElementIsNull(x, y) || !board.IsColumnClearAbove(x, y))
                return;

            // Create a random new tile 
            var tileItem = CreateTile(x, y);

            // Tile fall animation
            animationController.FallAnimation(tileItem, new Vector2Int(x, y + fallOffset), new Vector2Int(x, y));
        });
    }
}