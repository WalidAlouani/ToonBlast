using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionScreen : MainMenuView
{
    [SerializeField] private Button backButton;
    [SerializeField] private LevelsDataSO levelsData;
    [SerializeField] private ButtonLevel buttonLevelPrefab;
    [SerializeField] private RectTransform container;

    private void Start()
    {
        backButton.onClick.AddListener(() => navigation.Show<MainScreen>());
        InitializeLevelButtons();
    }

    private void InitializeLevelButtons()
    {
        for (int i = 0; i < levelsData.LevelsNumbers.Count; i++)
        {
            var button = Instantiate(buttonLevelPrefab, container);
            var levelNumber = levelsData.LevelsNumbers[i];
            var isClickable = levelNumber <= levelsData.LastUnlockedLevel;
            button.Init(levelNumber.ToString(), isClickable,() => OnClick(levelNumber));
        }
    }

    private void OnClick(int levelNumber)
    {
        levelsData.SelectedLevel = levelNumber;
        SceneLoader.Instance.LoadLevel("Game");
    }
}
