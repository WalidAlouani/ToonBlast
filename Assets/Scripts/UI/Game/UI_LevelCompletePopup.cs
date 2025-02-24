using UnityEngine;
using UnityEngine.UI;

public class UI_LevelCompletePopup : MonoBehaviour
{
    [SerializeField] private LevelDataManager levelDataManager;
    [SerializeField] private Button closeButton;
    [SerializeField] private SoundTrigger closeButtonSound;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private SoundTrigger nextLevelButtonSound;

    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(closeButtonSound);
            SceneLoader.Instance.LoadMenuScene();
        });

        nextLevelButton.onClick.AddListener(() =>
        {
            levelDataManager.SelectNextLevel();
            AudioManager.Instance.PlaySound(nextLevelButtonSound);
            SceneLoader.Instance.LoadGameScene();
        });
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveAllListeners();
        nextLevelButton.onClick.RemoveAllListeners();
    }
}
