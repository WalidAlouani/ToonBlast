using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GoalManager goalManager;

    private void OnEnable()
    {
        var eventManager = ServiceLocator.Get<EventManager>();
        eventManager.OnTilesDestroyed.Subscribe(OnTilesDestroyed);
        eventManager.OnGameStateChanged.Subscribe(OnGameStateChanged);
        goalManager.OnGoalCompleted += OnGoalCompleted;
    }

    private void OnDisable()
    {
        if (ServiceLocator.TryGet<EventManager>(out var eventManager))
        {
            eventManager.OnTilesDestroyed.Unsubscribe(OnTilesDestroyed);
            eventManager.OnGameStateChanged.Unsubscribe(OnGameStateChanged);
        }
        goalManager.OnGoalCompleted -= OnGoalCompleted;
    }

    private void OnTilesDestroyed(List<TileItem> list)
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
