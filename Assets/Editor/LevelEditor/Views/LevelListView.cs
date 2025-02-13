using UnityEngine;
using UnityEditor;
using System;

namespace Tools.LevelEditor
{
    public class LevelListView : ILevelEditorView
    {
        private LevelEditorController controller;
        private readonly Action<LevelEditorScreen> changeView;
        private Vector2 scrollPos;

        public LevelListView(LevelEditorController controller, Action<LevelEditorScreen> changeView)
        {
            this.controller = controller;
            this.changeView = changeView;
        }

        public void OnEnter() { }

        public void OnExit() { }

        public void OnRender()
        {
            DisplayLevelsList();
            DisplayCreateLevel();
        }

        private void DisplayLevelsList()
        {
            GUILayout.Space(10);
            GUILayout.Label("Levels List", EditorStyles.boldLabel);

            // Refresh Button
            if (GUILayout.Button("Refresh Level List"))
            {
                controller.RefreshLevelList();
            }

            // Display available level files
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(400));
            for (int i = 0; i < controller.LevelFiles.Count; i++)
            {
                GUILayout.BeginHorizontal();

                var fileName = controller.LevelFiles[i];

                // Load level button
                if (GUILayout.Button(fileName, EditorStyles.miniButtonLeft))
                {
                    controller.LoadLevel(fileName);
                    changeView.Invoke(LevelEditorScreen.LevelEditor);
                }

                // Delete level button
                if (GUILayout.Button("X", EditorStyles.miniButtonRight, GUILayout.Width(25)))
                {
                    if (EditorUtility.DisplayDialog("Delete Level", $"Are you sure you want to delete {fileName}?", "Yes", "No"))
                    {
                        controller.DeleteLevel(fileName);
                    }
                }

                GUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }

        private void DisplayCreateLevel()
        {
            // Create New Level
            GUILayout.Space(10);
            if (GUILayout.Button("Create New Level"))
            {
                changeView.Invoke(LevelEditorScreen.CreateLevel);
            }
        }
    }
}
