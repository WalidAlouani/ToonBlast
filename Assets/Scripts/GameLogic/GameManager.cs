using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private LevelDataManager levelDataManager;
    [SerializeField] private GameScreenFitter gameScreenFitter;
    [SerializeField] private MovesManager movesManager;
    [SerializeField] private GoalManager goalManager;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private MatchingManager matchingManager;

    private void Awake()
    {
        gameStateManager.ChangeState(GameState.Loading);
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
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
        gridManager.Init(data.Width, data.Height);
        boardManager.Init(data.Width, data.Height, data.Tiles, data.ActiveTypes);
        matchingManager.Init(data.Width, data.Height);
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
        if (goalManager.GoalCompleted)
            return;

        gameStateManager.ChangeState(GameState.GameOver);
        gameStateManager.EnqueueStateChange(GameState.RetryPopup, 2);
        //Debug.Log("Out Of Moves");
    }
}
