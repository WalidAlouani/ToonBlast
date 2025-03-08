using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private LevelDataManager levelDataManager;
    [SerializeField] private GameScreenFitter gameScreenFitter;
    [SerializeField] private TileFactory tileFactory;
    [SerializeField] private BoardManager boardManager;

    private MovesManager movesManager;
    private GoalManager goalManager;

    private void Awake()
    {
        movesManager = new MovesManager();
        goalManager = new GoalManager();
        gameStateManager.ChangeState(GameState.Loading);
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
        movesManager?.Dispose();
        goalManager?.Dispose();
    }

    private void SubscribeEvents()
    {
        levelDataManager.OnLevelInfoLoaded += OnLevelInfoLoaded;
        goalManager.OnAllGoalsCompleted += OnGoalsCompleted;
        movesManager.OnOutOfMoves += OnOutOfMoves;
    }

    private void UnsubscribeEvents()
    {
        levelDataManager.OnLevelInfoLoaded -= OnLevelInfoLoaded;
        goalManager.OnAllGoalsCompleted -= OnGoalsCompleted;
        movesManager.OnOutOfMoves -= OnOutOfMoves;
    }

    private void OnLevelInfoLoaded(LevelData data)
    {
        SetupLevel(data);
        gameStateManager.ChangeState(GameState.WaitScreen);
        gameStateManager.EnqueueStateChange(GameState.Playing, 2);
    }

    private void SetupLevel(LevelData data)
    {
        gameScreenFitter.Init(data.Width, data.Height);
        movesManager.Init(data.MaxMoves);
        goalManager.Init(data.LevelGoals);
        tileFactory.Init(data.ActiveTypes);
        boardManager.Init(data.Width, data.Height, data.Tiles);
    }

    private void OnGoalsCompleted()
    {
        gameStateManager.ChangeState(GameState.LevelCompleted);
        gameStateManager.EnqueueStateChange(GameState.NextLevelPopup, 2);
        levelDataManager.UpdateReachedLevel();
        //Debug.Log("All Goal Completed");
    }

    private void OnOutOfMoves()
    {
        if (gameStateManager.CurrentState == GameState.LevelCompleted)
            return;

        gameStateManager.ChangeState(GameState.GameOver);
        gameStateManager.EnqueueStateChange(GameState.RetryPopup, 2);
        //Debug.Log("Out Of Moves");
    }
}
