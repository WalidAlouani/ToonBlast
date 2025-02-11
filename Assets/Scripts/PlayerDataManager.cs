using UnityEngine;

public class PlayerDataManager
{
    public int GetLastUnlockedLevel()
    {
        return PlayerPrefs.GetInt("UnlockedLevel", 1);
    }

    public void SaveLastUnlockedLevel(int unlockedLevel)
    {
        PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);
    }

    public void ClearAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
