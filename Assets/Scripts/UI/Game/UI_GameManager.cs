using UnityEngine;
using UnityEngine.UI;

public class UI_GameManager : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private UI_LevelCompletePopup levelCompletePopup;
    [SerializeField] private UI_OutOfMovesPopup outOfMovesPopup;
    [SerializeField] private UI_QuitPopup quitPopup;
    [SerializeField] private Button backButton;

    void OnEnable()
    {
        backButton.onClick.AddListener(() =>
        {
            if (gameStateManager.CurrentState == GameState.Playing)
                gameStateManager.ChangeState(GameState.Paused);
        });

        ServiceLocator.Get<EventManager>().OnGameStateChanged.Subscribe(OnGameStateChanged, this);
    }

    private void OnDisable()
    {
        if (ServiceLocator.TryGet<EventManager>(out var eventManager))
            eventManager.OnGameStateChanged.Unsubscribe(OnGameStateChanged);

        backButton.onClick.RemoveAllListeners();
    }

    private void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                quitPopup.gameObject.SetActive(false);
                break;
            case GameState.Paused:
                quitPopup.gameObject.SetActive(true);
                break;
            case GameState.NextLevelPopup:
                levelCompletePopup.gameObject.SetActive(true);
                break;
            case GameState.RetryPopup:
                outOfMovesPopup.gameObject.SetActive(true);
                break;
        }
    }
}
