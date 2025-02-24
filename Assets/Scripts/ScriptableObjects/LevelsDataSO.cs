using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsDataHolder", menuName = "ScriptableObjects/LevelsDataHolder", order = 3)]
public class LevelsDataSO : ScriptableObject
{
    public List<int> LevelsNumbers;
    public int LastUnlockedLevel = 1;
    public int SelectedLevel = 1;
}
