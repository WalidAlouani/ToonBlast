using System;
using UnityEngine;

public class PlayerDataManager
{
    public int GetLastUnlockedLevel()
    {
        return PlayerPrefs.GetInt("UnlockedLevel", 1);
    }

    public void SaveLastUnlockedLevel(int unlockedLevel)
    {
        if (unlockedLevel <= GetLastUnlockedLevel())
            return;

        PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);
    }

    public bool LoadIsSoundMuted()
    {
        return PlayerPrefs.GetInt("IsSoundMuted", 0) == 1;
    }

    public void SaveIsSoundMuted(bool isMuted)
    {
        PlayerPrefs.SetInt("IsSoundMuted", isMuted ? 1 : 0);
    }

    public void ClearAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
