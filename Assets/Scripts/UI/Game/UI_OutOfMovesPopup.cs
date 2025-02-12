using UnityEngine;
using UnityEngine.UI;

public class UI_OutOfMovesPopup : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button tryAgainButton;

    private void Start()
    {
        closeButton.onClick.AddListener(() => SceneLoader.Instance.LoadMenuScene());
        tryAgainButton.onClick.AddListener(() => { SceneLoader.Instance.LoadGameScene(); });
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveAllListeners();
        tryAgainButton.onClick.RemoveAllListeners();
    }
}
