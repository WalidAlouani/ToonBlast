using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Tools.LevelEditor
{
    public class LevelEditorView : ILevelEditorView
    {
        private readonly Action<LevelEditorScreen> changeView;
        private readonly LevelEditorSO config;
        private LevelEditorController controller;
        private LevelData currentLevel;
        private ItemType selectedItemType;
        private Dictionary<ItemType, ItemTypeSO> availableItems;
        private Vector2 scrollPos;
        private ItemType[] allItemTypes;

        public LevelEditorView(LevelEditorController controller, Action<LevelEditorScreen> changeView)
        {
            this.controller = controller;
            this.changeView = changeView;
            config = controller.Config;
            availableItems = config.ItemInventory.ItemsByTypes;
            availableItems[ItemType.None] = config.RandomItem;

            // Cache enum values (excluding None)
            allItemTypes = EnumUtils.GetValues<ItemType>().Skip(1).ToArray();
        }

        public void OnEnter()
        {
            currentLevel = controller.CurrentLevel;
            selectedItemType = ItemType.None;
        }

        public void OnExit()
        {
            GUI.FocusControl(null);
        }

        public void OnRender()
        {
            if (currentLevel == null)
                return;

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            RenderLevelProperties();
            RenderLevelGoals();
            RenderActiveItemTypes();
            RenderItemSelectionPanel();
            RenderGrid();
            RenderControlButtons();

            EditorGUILayout.EndScrollView();
        }

        private void RenderLevelProperties()
        {
            GUILayout.Space(10);
            GUILayout.Label("Editing Level", EditorStyles.boldLabel);

            currentLevel.Number = EditorGUILayout.IntField("Number", currentLevel.Number);
            currentLevel.Width = EditorGUILayout.IntSlider("Width", currentLevel.Width, config.MinGridWidth, config.MaxGridWidth);
            currentLevel.Height = EditorGUILayout.IntSlider("Height", currentLevel.Height, config.MinGridHeight, config.MaxGridHeight);
            currentLevel.MaxMoves = EditorGUILayout.IntField("Moves", currentLevel.MaxMoves);
        }

        private void RenderLevelGoals()
        {
            GUILayout.Space(10);
            GUILayout.Label("Level Goals", EditorStyles.boldLabel);

            // Convert active types to an array for use in the popup
            var activeTypes = currentLevel.ActiveTypes.ToArray();
            string[] options = activeTypes.Select(t => t.ToString()).ToArray();

            for (int i = 0; i < currentLevel.LevelGoals.Count; i++)
            {
                GUILayout.BeginHorizontal();

                // Find the index of the current level goal's type within the active types
                int currentIndex = Array.IndexOf(activeTypes, currentLevel.LevelGoals[i].ItemType);

                // If the current type isn't available (case of removed type), default to the first active type
                if (currentIndex < 0)
                    currentIndex = 0;

                // Display the popup using only active types
                int selectedIndex = EditorGUILayout.Popup("Item Type", currentIndex, options);
                currentLevel.LevelGoals[i].ItemType = activeTypes[selectedIndex];

                currentLevel.LevelGoals[i].Count = EditorGUILayout.IntField("Count", currentLevel.LevelGoals[i].Count);

                // Remove button
                if (GUILayout.Button("X", GUILayout.Width(25)))
                {
                    currentLevel.LevelGoals.RemoveAt(i);
                    break;
                }
                GUILayout.EndHorizontal();
            }

            // Add new goal button
            if (GUILayout.Button("+ Add Goal"))
            {
                currentLevel.LevelGoals.Add(new LevelGoal());
            }
        }

        private void RenderActiveItemTypes()
        {
            GUILayout.Space(10);
            GUILayout.Label("Active Item Types", EditorStyles.boldLabel);

            for (int i = 0; i < currentLevel.ActiveTypes.Count; i++)
            {
                GUILayout.BeginHorizontal();

                int currentIndex = Array.IndexOf(allItemTypes, currentLevel.ActiveTypes[i]);
                if (currentIndex < 0) currentIndex = 0;

                int selectedIndex = EditorGUILayout.Popup("Item Type", currentIndex, allItemTypes.Select(e => e.ToString()).ToArray());
                currentLevel.ActiveTypes[i] = allItemTypes[selectedIndex];

                if (currentLevel.ActiveTypes.Count > config.MinTypeCountPerLevel)
                {
                    if (GUILayout.Button("X", GUILayout.Width(25)))
                    {
                        currentLevel.ActiveTypes.RemoveAt(i);
                        break;
                    }
                }

                GUILayout.EndHorizontal();
            }

            if (currentLevel.ActiveTypes.Count < allItemTypes.Length)
            {
                if (GUILayout.Button("+ Add Type"))
                {
                    var newType = allItemTypes.Except(currentLevel.ActiveTypes).First();
                    currentLevel.ActiveTypes.Add(newType);
                }
            }
        }

        private void RenderItemSelectionPanel()
        {
            GUILayout.Space(10);
            GUILayout.Label("Items Panel", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (!currentLevel.ActiveTypes.Contains(selectedItemType))
                selectedItemType = currentLevel.ActiveTypes[0];

            foreach (var item in availableItems)
            {
                if (!currentLevel.ActiveTypes.Contains(item.Key) && item.Key != ItemType.None)
                    continue;

                var size = selectedItemType == item.Key ? 75 : 60;
                if (GUILayout.Button(item.Value.Sprite.texture, GUILayout.Width(size), GUILayout.Height(size)))
                {
                    selectedItemType = item.Key;
                }
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void RenderGrid()
        {
            GUILayout.Space(10);
            GUILayout.Label("Grid:", EditorStyles.boldLabel);

            for (int y = currentLevel.Height - 1; y >= 0; y--)
            {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                for (int x = 0; x < currentLevel.Width; x++)
                {
                    if (!currentLevel.ActiveTypes.Contains(currentLevel.Tiles[x][y]))
                        currentLevel.Tiles[x][y] = ItemType.None;

                    var texture = availableItems[currentLevel.Tiles[x][y]].Sprite.texture;
                    if (GUILayout.Button(texture, GUILayout.Width(50), GUILayout.Height(50)))
                    {
                        currentLevel.Tiles[x][y] = selectedItemType;
                    }
                }

                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
        }

        private void RenderControlButtons()
        {
            GUILayout.Space(10);
            if (GUILayout.Button("Clear Grid"))
            {
                ClearGrid();
            }

            GUILayout.Space(10);
            if (GUILayout.Button("Save Level"))
            {
                controller.SaveLevel();
            }

            if (GUILayout.Button("Back"))
            {
                changeView.Invoke(LevelEditorScreen.LevelList);
            }
        }

        private void ClearGrid()
        {
            for (int y = 0; y < currentLevel.Height; y++)
            {
                for (int x = 0; x < currentLevel.Width; x++)
                {
                    currentLevel.Tiles[x][y] = ItemType.None;
                }
            }
        }
    }
}
