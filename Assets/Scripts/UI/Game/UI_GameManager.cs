using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameManager : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager; 
    [SerializeField] private UI_LevelCompletePopup levelCompletePopup; 
    [SerializeField] private UI_OutOfMovesPopup outOfMovesPopup; 
    [SerializeField] private Button backButton; 

    void Start()
    {
        backButton.onClick.AddListener(()=> SceneLoader.Instance.LoadMenuScene());
        gameStateManager.OnStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        gameStateManager.OnStateChanged -= OnGameStateChanged;
        backButton.onClick.RemoveAllListeners();
    }

    private void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.None:
                break;
            case GameState.Loading:
                break;
            case GameState.WaitScreen:
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
                levelCompletePopup.gameObject.SetActive(true);
                break;
            case GameState.RetryPopup:
                outOfMovesPopup.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}
