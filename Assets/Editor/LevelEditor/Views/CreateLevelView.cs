using UnityEngine;
using UnityEditor;
using System;

namespace Tools.LevelEditor
{
    public class CreateLevelView : ILevelEditorView
    {
        private LevelEditorController controller;
        private readonly Action<LevelEditorScreen> changeView;

        private LevelData currentLevel;

        public CreateLevelView(LevelEditorController controller, Action<LevelEditorScreen> changeView)
        {
            this.controller = controller;
            this.changeView = changeView;
        }

        public void OnEnter()
        {
            controller.CreateLevel();
            currentLevel = controller.CurrentLevel;
        }

        public void OnExit()
        {
            GUI.FocusControl(null);
        }

        public void OnRender()
        {
            GUILayout.Space(10);
            GUILayout.Label("Create New Level", EditorStyles.boldLabel);

            currentLevel.Number = EditorGUILayout.IntField("Number", currentLevel.Number);

            // Save Button
            if (GUILayout.Button("Create Level"))
            {
                if (controller.OverwriteCheck())
                {
                    bool overwrite = EditorUtility.DisplayDialog("Level already exist", $"A file called 'Level{currentLevel.Number}' already exists.\nDo you want to overwrite it?", "Yes", "No");
                    if (!overwrite)
                        return;
                }
                controller.SaveLevel();
                currentLevel.Number++;
            }

            if (GUILayout.Button("Back"))
            {
                changeView.Invoke(LevelEditorScreen.LevelList);
            }
        }
    }
}