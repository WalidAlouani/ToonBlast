using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelFileHandler
{
    private string levelsDirectory;
    private IDataSerializer<LevelData> serializer;

    public LevelFileHandler(IDataSerializer<LevelData> serializer, string directory)
    {
        this.serializer = serializer;
        levelsDirectory = Path.Combine(Application.streamingAssetsPath, directory);

        if (!Directory.Exists(levelsDirectory))
        {
            Directory.CreateDirectory(levelsDirectory);
        }
    }

    public List<string> GetLevelsNames()
    {
        var files = Directory.GetFiles(levelsDirectory, "*." + serializer.FileExtension);

        return files.Select(Path.GetFileName)
            .OrderBy(FileUtils.ExtractLevelNumber)
            .ToList();
    }

    public List<int> GetLevelsNumbers()
    {
        var files = Directory.GetFiles(levelsDirectory, "*." + serializer.FileExtension);

        return files.Select(FileUtils.ExtractLevelNumber)
            .OrderBy(el => el)
            .ToList();
    }

    public bool IsLevelFileExist(LevelData level)
    {
        string path = Path.Combine(levelsDirectory, $"level{level.Number}." + serializer.FileExtension);
        return File.Exists(path);
    }

    public LevelData LoadLevel(string filename)
    {
        string path = Path.Combine(levelsDirectory, filename);
        return serializer.Load(path);
    }

    public LevelData LoadLevel(int levelNumber)
    {
        string path = Path.Combine(levelsDirectory, $"level{levelNumber}." + serializer.FileExtension);
        return serializer.Load(path);
    }

    public void SaveLevel(LevelData level)
    {
        string path = Path.Combine(levelsDirectory, $"level{level.Number}." + serializer.FileExtension);
        serializer.Save(path, level);

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }

    public void DeleteLevel(string filename)
    {
        string path = Path.Combine(levelsDirectory, filename);
        if (!File.Exists(path))
        {
            Debug.LogWarning("Level to delete not found: " + path);
            return;
        }

        File.Delete(path);
        File.Delete(path + ".meta");

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif

        Debug.Log("Level deleted: " + path);
    }

    public int MaxLevelNumber()
    {
        var levelFiles = GetLevelsNumbers();
        if (levelFiles.Count <= 0)
            return 0;

        return levelFiles.Max();
    }
}
