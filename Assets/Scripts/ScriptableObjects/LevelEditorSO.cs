using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelEditorConfig", menuName = "ScriptableObjects/LevelEditorConfig", order = 1)]
public class LevelEditorSO : ScriptableObject
{
    public string SaveDirectory;

    [Header("Grid Section")]
    public int MinGridWidth;
    public int MinGridHeight;
    public int MaxGridWidth;
    public int MaxGridHeight;

    [Header("Items Section")]
    public ItemTypeSO RandomItem;
    public ItemInventorySO ItemInventory;
    public int MinTypeCountPerLevel;
}
