using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuitPopup : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private Button closeButton;
    [SerializeField] private SoundTrigger closeButtonSound;
    [SerializeField] private Button quitButton;
    [SerializeField] private SoundTrigger quitButtonSound;

    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(closeButtonSound);
            gameStateManager.ChangeState(GameState.Playing);
        });

        quitButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(quitButtonSound);
            SceneLoader.Instance.LoadMenuScene();
        });
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }
}
