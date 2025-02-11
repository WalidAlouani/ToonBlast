using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelDataLoader levelDataLoader;
    [SerializeField] private GameScreenFitter gameScreenFitter;
    [SerializeField] private MovesManager movesManager;
    [SerializeField] private GoalManager goalManager;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private MatchingManager matchingManager;

    private void Awake()
    {
        levelDataLoader.OnLevelInfoLoaded += OnLevelInfoLoaded;
    }

    private void OnDestroy()
    {
        levelDataLoader.OnLevelInfoLoaded -= OnLevelInfoLoaded;
    }

    private void OnLevelInfoLoaded(LevelData data)
    {
        gameScreenFitter.Init(data.Width, data.Height);
        movesManager.Init(data.MaxMoves);
        goalManager.Init(data.LevelGoals);
        gridManager.Init(data.Width, data.Height);
        boardManager.Init(data.Width, data.Height, data.Tiles);
        matchingManager.Init(data.Width, data.Height);
    }
}
