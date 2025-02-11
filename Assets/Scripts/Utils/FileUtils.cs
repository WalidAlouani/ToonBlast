using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public static class FileUtils
{
    // Extracts the numeric part from "level1.json" → 1, "level10.json" → 10
    public static int ExtractLevelNumber(string filePath)
    {
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        Match match = Regex.Match(fileName, @"\d+"); // Extract digits

        return match.Success ? int.Parse(match.Value) : int.MaxValue;
    }

    public static string ToProjectRelativePath(string absolutePath)
    {
        string projectRoot = Application.dataPath.Substring(0, Application.dataPath.Length - "Assets".Length);
        string relativePath = absolutePath.Replace(projectRoot, "").Replace("\\", "/");
        return relativePath;
    }
}
