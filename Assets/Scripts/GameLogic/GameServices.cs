using UnityEngine;

public class GameServices : MonoBehaviour
{
    [SerializeField] private LevelEditorSO levelsConfig;

    private void Awake()
    {
        ServiceLocator.Register(new LevelFileHandler(new LevelSerializer(), levelsConfig.SaveDirectory));
        ServiceLocator.Register(new PlayerDataManager());
    }

    private void OnApplicationQuit()
    {
        ServiceLocator.Clear();
    }
}
