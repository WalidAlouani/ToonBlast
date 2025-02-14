using UnityEngine;
using UnityEngine.UI;

public class UI_OutOfMovesPopup : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button tryAgainButton;

    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(SoundTrigger.CloseButton);
            SceneLoader.Instance.LoadMenuScene();

        });
        tryAgainButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(SoundTrigger.ClickButton);
            SceneLoader.Instance.LoadGameScene();
        });
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveAllListeners();
        tryAgainButton.onClick.RemoveAllListeners();
    }
}
