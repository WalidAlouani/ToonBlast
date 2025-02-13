using UnityEngine;

public class GameServices : MonoBehaviour
{
    [SerializeField] private LevelEditorSO levelsConfig;

    private void Awake()
    {
        if (!ServiceLocator.HasService<LevelFileHandler>())
            ServiceLocator.Register(new LevelFileHandler(new LevelSerializer(), levelsConfig.SaveDirectory));
        if (!ServiceLocator.HasService<PlayerDataManager>())
            ServiceLocator.Register(new PlayerDataManager());
    }

    private void OnApplicationQuit()
    {
        ServiceLocator.Clear();
    }
}
