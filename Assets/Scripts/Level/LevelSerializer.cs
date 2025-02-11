using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class LevelSerializer : IDataSerializer<LevelData>
{
    public void Save(string path, LevelData level)
    {
        string json = JsonConvert.SerializeObject(level);
        File.WriteAllText(path, json);
        Debug.Log($"Saved level: {path}");
    }

    public LevelData Load(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning($"Level to load not found: {path}");
            return null;
        }

        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<LevelData>(json);
    }
}
