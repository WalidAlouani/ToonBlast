using System.Collections.Generic;

namespace Tools.LevelEditor
{
    public class LevelEditorController
    {
        public LevelEditorSO Config { get; private set; }
        public List<string> LevelFiles { get; private set; }
        public LevelData CurrentLevel { get; private set; }

        private LevelFileHandler fileHandler;

        public LevelEditorController(LevelEditorSO config)
        {
            fileHandler = new LevelFileHandler(new LevelSerializer(), config.SaveDirectory);
            Config = config;
            RefreshLevelList();
        }

        public void RefreshLevelList()
        {
            LevelFiles = fileHandler.GetLevelsNames();
        }

        public void LoadLevel(string fileName)
        {
            CurrentLevel = fileHandler.LoadLevel(fileName);
        }

        public void SaveLevel()
        {
            if (CurrentLevel == null)
                return;

            fileHandler.SaveLevel(CurrentLevel);
            RefreshLevelList();
        }

        public void DeleteLevel(string fileName)
        {
            fileHandler.DeleteLevel(fileName);
            RefreshLevelList();
        }

        public void CreateLevel()
        {
            var levelNumber = fileHandler.MaxLevelNumber() + 1;
            CurrentLevel = new LevelData(levelNumber, Config.MinGridWidth, Config.MinGridHeight);
        }

        public bool OverwriteCheck()
        {
            if (CurrentLevel == null)
                return false;

            return fileHandler.IsLevelFileExist(CurrentLevel);
        }
    }
}