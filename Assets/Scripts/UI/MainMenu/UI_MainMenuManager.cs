using UnityEngine;

public class UI_MainMenuManager : MonoBehaviour
{
    [SerializeField] private LevelsDataSO levelDataHolder;

    private void Start()
    {
        var levelsManager = ServiceLocator.Get<LevelFileHandler>();
        var playerData = ServiceLocator.Get<PlayerDataManager>();

        levelDataHolder.LevelsNames = levelsManager.GetLevelsNames();
        levelDataHolder.LevelsNumbers = levelsManager.GetLevelsNumbers();
        levelDataHolder.LastUnlockedLevel = playerData.GetLastUnlockedLevel();
    }
}
