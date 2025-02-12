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
        var playerData = ServiceLocator.Get<PlayerDataManager>();
        playerData.SaveLastUnlockedLevel(LevelData.Number + 1);
    }

    public void SelectNextLevel()
    {
        levelDataHolder.SelectedLevel = LevelData.Number + 1;
    }
}
