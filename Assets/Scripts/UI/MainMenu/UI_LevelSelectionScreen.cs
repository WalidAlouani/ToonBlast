using UnityEngine;
using UnityEngine.UI;

public class UI_LevelSelectionScreen : MainMenuView
{
    [SerializeField] private Button backButton;
    [SerializeField] private LevelsDataSO levelsData;
    [SerializeField] private UI_ButtonLevel buttonLevelPrefab;
    [SerializeField] private RectTransform container;

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            navigation.Show<UI_MainScreen>();
            AudioManager.Instance.PlaySound(SoundTrigger.BackButton);
        });
        InitializeLevelButtons();
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
    }

    private void InitializeLevelButtons()
    {
        for (int i = 0; i < levelsData.LevelsNumbers.Count; i++)
        {
            var button = Instantiate(buttonLevelPrefab, container);
            var levelNumber = levelsData.LevelsNumbers[i];
            var isUnlocked = levelNumber <= levelsData.LastUnlockedLevel;
            button.Init(levelNumber.ToString(), isUnlocked, () => OnClick(levelNumber));
        }
    }

    private void OnClick(int levelNumber)
    {
        levelsData.SelectedLevel = levelNumber;
        SceneLoader.Instance.LoadGameScene();
        AudioManager.Instance.PlaySound(SoundTrigger.ClickButton);
    }
}
