using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_FadeInOutScreen : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private Image image;

    private void Awake()
    {
        gameStateManager.OnStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        gameStateManager.OnStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                FadeOut();
                break;
            case GameState.Loading:
            case GameState.Paused:
            case GameState.NextLevelPopup:
            case GameState.RetryPopup:
                FadeIn();
                break;
        }
    }

    private void FadeIn()
    {
        gameObject.SetActive(true);
        image.DOFade(0.5f, 0.25f);
    }

    private void FadeOut()
    {
        image.DOFade(0, 0.25f).OnComplete(() => gameObject.SetActive(false));
    }
}
