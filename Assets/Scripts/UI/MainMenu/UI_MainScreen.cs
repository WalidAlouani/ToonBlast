using UnityEngine;
using UnityEngine.UI;

public class UI_MainScreen : MainMenuView
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private UI_SettingsPopup settingsPopup;

    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            navigation.Show<UI_LevelSelectionScreen>();
            AudioManager.Instance.PlaySound(SoundTrigger.ClickButton);
        });

        settingsButton.onClick.AddListener(() =>
        {
            settingsPopup.gameObject.SetActive(true);
            AudioManager.Instance.PlaySound(SoundTrigger.ClickButton);
        });
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
    }
}
