using UnityEngine;

public class GameServices : MonoBehaviour
{
    [SerializeField] private LevelEditorSO levelsConfig;

    private void Awake()
    {
        var levelSerializer = LevelSerializerUtils.GetLevelDataSerializer(levelsConfig.SerializerType);
        ServiceLocator.Register(new LevelFileHandler(levelSerializer, levelsConfig.SaveDirectory));
        ServiceLocator.Register<IPlayerDataManager>(new PlayerDataManager());
        ServiceLocator.Register(new EventManager());
    }

    private void OnApplicationQuit()
    {
        ServiceLocator.Clear();
    }
}
