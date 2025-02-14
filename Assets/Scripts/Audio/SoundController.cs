using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private GoalManager goalManager;
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private AudioManager audioManager;

    private void Awake()
    {
        gameStateManager.OnStateChanged += OnGameStateChanged;
        boardManager.OnTilesDestroyed += OnTilesDestroyed;
        goalManager.OnGoalCompleted += OnGoalCompleted;
    }

    private void OnDestroy()
    {
        gameStateManager.OnStateChanged -= OnGameStateChanged;
        boardManager.OnTilesDestroyed -= OnTilesDestroyed;
        goalManager.OnGoalCompleted -= OnGoalCompleted;
    }

    private void OnTilesDestroyed(Vector2Int @int, List<TileItem> list)
    {
        if (list.Count > 5)
            audioManager.PlaySound(SoundTrigger.TileBreak5);
        else
            audioManager.PlaySound(SoundTrigger.TileBreak);
    }

    private void OnGoalCompleted(LevelGoal levelGoal)
    {
        audioManager.PlaySound(SoundTrigger.GoalReached);
    }

    private void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Loading:
                break;
            case GameState.WaitScreen:
                audioManager.PlaySound(SoundTrigger.StartGame);
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.LevelCompleted:
                break;
            case GameState.GameOver:
                break;
            case GameState.NextLevelPopup:
                audioManager.PlaySound(SoundTrigger.LevelCompleted);
                break;
            case GameState.RetryPopup:
                audioManager.PlaySound(SoundTrigger.OutOfMoves);
                break;
        }
    }
}
