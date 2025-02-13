using UnityEngine;
using UnityEngine.UI;

public class UI_MainScreen : MainMenuView
{
    [SerializeField] private Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            navigation.Show<UI_LevelSelectionScreen>();
            AudioManager.Instance.PlaySound(SoundTrigger.ClickButton);
        });
    }
}
