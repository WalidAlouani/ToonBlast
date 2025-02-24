using UnityEngine;
using UnityEngine.UI;

public class UI_OutOfMovesPopup : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private SoundTrigger closeButtonSound;
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private SoundTrigger tryAgainButtonSound;

    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(closeButtonSound);
            SceneLoader.Instance.LoadMenuScene();

        });
        tryAgainButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(tryAgainButtonSound);
            SceneLoader.Instance.LoadGameScene();
        });
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveAllListeners();
        tryAgainButton.onClick.RemoveAllListeners();
    }
}
