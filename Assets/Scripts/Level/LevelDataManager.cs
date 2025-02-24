using System;
using UnityEngine;

public class LevelDataManager : MonoBehaviour
{
    [SerializeField] private LevelsDataSO levelDataHolder;
    public LevelData LevelData { get; private set; }

    public Action<LevelData> OnLevelInfoLoaded;

    void Start()
    {
        LoadLevelData();
    }

    private void LoadLevelData()
    {
        var fileHandler = ServiceLocator.Get<LevelFileHandler>();
        LevelData = fileHandler.LoadLevel(levelDataHolder.SelectedLevel);
        OnLevelInfoLoaded?.Invoke(LevelData);
    }

    public void UpdateReachedLevel()
    {
        var playerData = ServiceLocator.Get<IPlayerDataManager>();
        playerData.SaveLastUnlockedLevel(LevelData.Number + 1);
    }

    public void SelectNextLevel()
    {
        var nextLevelNumber = LevelData.Number + 1;

        // if we reached the last level we keep playing it
        if (nextLevelNumber > levelDataHolder.LevelsNumbers.Count)
            return;

        levelDataHolder.SelectedLevel = nextLevelNumber;
    }
}
