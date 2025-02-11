using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MainMenuView
{
    [SerializeField] private Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(() => navigation.Show<LevelSelectionScreen>());
    }
}
