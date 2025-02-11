using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsDataHolder", menuName = "ScriptableObjects/LevelsDataHolder", order = 3)]
public class LevelsDataSO : ScriptableObject
{
    public List<string> LevelsNames;// remove if not used
    public List<int> LevelsNumbers;
    public int LastUnlockedLevel;
    public int SelectedLevel;
}
