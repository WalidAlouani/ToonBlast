using UnityEngine;

[CreateAssetMenu(fileName = "LevelEditorConfig", menuName = "ScriptableObjects/LevelEditorConfig", order = 1)]
public class LevelEditorSO : ScriptableObject
{
    [Header("Storage Section")]
    public string SaveDirectory;
    public LevelSerializerType SerializerType;

    [Header("Grid Section")]
    public int MinGridWidth;
    public int MinGridHeight;
    public int MaxGridWidth;
    public int MaxGridHeight;

    [Header("Items Section")]
    public ItemInventorySO ItemInventory;
    public int MinTypeCountPerLevel;

    [Header("Goals Section")]
    public int MinGoalCount;
}
