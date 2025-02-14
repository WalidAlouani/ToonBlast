using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuitPopup : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(SoundTrigger.CloseButton);
            gameStateManager.ChangeState(GameState.Playing);
        });

        quitButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(SoundTrigger.ClickButton);
            SceneLoader.Instance.LoadMenuScene();
        });
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }
}
