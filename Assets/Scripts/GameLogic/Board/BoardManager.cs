using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private TileFactory tileFactory;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private int fallOffset = 10;
    [SerializeField] private float fallDuration = 0.3f;
    [SerializeField] private float rearrangeDuration = 0.3f;

    private TileBoard board;

    // Add Board state manager
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

    private TileItem CreateTile(int x, int y, ItemType itemType)
    {
        var tileItem = tileFactory.CreateTile(x, y, itemType);
        board.SetElement(tileItem);

        tileItem.OnClick += OnTileClicked;
        tileItem.OnDestroy += OnTileDestoyed;

        return tileItem;
    }

    private void OnGridClicked(Vector2Int position)
    {
        if (blockInputs) return;

        var clickedTile = board.GetElement(position.x, position.y);

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

    private void OnTileDestoyed(TileItem tileItem)
    {
        tileItem.OnClick -= OnTileClicked;
        tileItem.OnDestroy -= OnTileDestoyed;

        if (!board.IsEqualTo(tileItem.X, tileItem.Y, tileItem))
            return;

        board.RemoveElement(tileItem.X, tileItem.Y);
    }

    private IEnumerator RearrangeAndRefill()
    {
        blockInputs = true;
        yield return new WaitForSeconds(Mathf.Max(rearrangeDuration, fallDuration));
        RearrangeBoard();
        RefillBoard();
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
            board.GetElement(x, targetY).SetPosition(x, targetY, rearrangeDuration);
        });
    }

    private void RefillBoard()
    {
        board.ForEach((x, y) =>
        {
            if (!board.ElementIsNull(x, y) || !board.IsColumnClearAbove(x, y))
                return;

            // Create a random new tile with a falling effect (offset position)
            var tileItem = CreateTile(x, y, ItemType.None); ;

            tileItem.SetPosition(x, y + fallOffset);
            tileItem.SetPosition(x, y, fallDuration);
        });
    }
}