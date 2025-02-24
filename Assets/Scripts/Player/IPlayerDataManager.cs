public interface IPlayerDataManager
{
    int GetLastUnlockedLevel();
    void SaveLastUnlockedLevel(int unlockedLevel);
    bool LoadIsSoundMuted();
    void SaveIsSoundMuted(bool isMuted);
}