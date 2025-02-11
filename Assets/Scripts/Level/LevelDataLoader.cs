using System;
using UnityEngine;

public class LevelDataLoader : MonoBehaviour
{
    [SerializeField] private LevelsDataSO levelDataHolder;
    public LevelData LevelData {  get; private set; }

    public Action<LevelData> OnLevelInfoLoaded;

    void Start()
    {
        var fileHandler = ServiceLocator.Get<LevelFileHandler>();
        LevelData = fileHandler.LoadLevel(levelDataHolder.SelectedLevel);
        OnLevelInfoLoaded?.Invoke(LevelData);
    }
}
