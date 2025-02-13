using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Linq;

public class LevelEditorView : ILevelEditorView
{
    private LevelEditorController controller;
    private readonly Action<LevelEditorScreen> changeView;

    private readonly LevelEditorSO config;

    private LevelData currentLevel;
    private ItemType selectedItemType;
    private Dictionary<ItemType, ItemTypeSO> availableItems;

    public LevelEditorView(LevelEditorController controller, Action<LevelEditorScreen> changeView)
    {
        this.controller = controller;
        this.changeView = changeView;
        config = controller.Config;
        availableItems = config.ItemInventory.ItemsByTypes;
        availableItems[ItemType.None] = config.RandomItem;
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
        // If a level is loaded, display its properties and grid
        if (currentLevel == null)
            return;

        GUILayout.Space(10);
        GUILayout.Label("Editing Level", EditorStyles.boldLabel);
        currentLevel.Number = EditorGUILayout.IntField("Number", currentLevel.Number);
        currentLevel.Width = EditorGUILayout.IntSlider("Width", currentLevel.Width, config.MinGridWidth, config.MaxGridWidth);
        currentLevel.Height = EditorGUILayout.IntSlider("Height", currentLevel.Height, config.MinGridHeight, config.MaxGridHeight);
        currentLevel.MaxMoves = EditorGUILayout.IntField("Moves", currentLevel.MaxMoves);

        GUILayout.Space(10);
        GUILayout.Label("Level Goals", EditorStyles.boldLabel);
        // Display the goal list
        for (int i = 0; i < currentLevel.LevelGoals.Count; i++)
        {
            GUILayout.BeginHorizontal();
            currentLevel.LevelGoals[i].ItemType = (ItemType)EditorGUILayout.EnumPopup("Item Type", currentLevel.LevelGoals[i].ItemType);
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

        GUILayout.Space(10);
        GUILayout.Label("Active Item Types", EditorStyles.boldLabel);
        var allTypes = EnumUtils.GetValues<ItemType>().Skip(1).ToArray();

        // Display the active types list
        for (int i = 0; i < currentLevel.ActiveTypes.Count; i++)
        {
            GUILayout.BeginHorizontal();

            string[] options = allTypes.Select(e => e.ToString()).ToArray();

            int currentIndex = Array.IndexOf(allTypes, currentLevel.ActiveTypes[i]);
            if (currentIndex < 0)
                currentIndex = 0;

            int selectedIndex = EditorGUILayout.Popup("Item Type", currentIndex, options);

            currentLevel.ActiveTypes[i] = allTypes[selectedIndex];

            // Remove button
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


        if (currentLevel.ActiveTypes.Count < allTypes.Length)
        {
            // Add new type button
            if (GUILayout.Button("+ Add Type"))
            {
                var newType = allTypes.Except(currentLevel.ActiveTypes).First();
                currentLevel.ActiveTypes.Add(newType);
            }
        }

        //Display Items Panel
        GUILayout.Space(10);
        GUILayout.Label("Items Panel", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (!currentLevel.ActiveTypes.Contains(selectedItemType))
            selectedItemType = currentLevel.ActiveTypes[0];

        foreach (var item in availableItems)
        {
            var itemType = item.Key;
            if (!currentLevel.ActiveTypes.Contains(itemType) && itemType != ItemType.None)
                continue;

            var size = selectedItemType == itemType ? 75 : 60;
            if (GUILayout.Button(item.Value.Sprite.texture, GUILayout.Width(size), GUILayout.Height(size)))
            {
                selectedItemType = itemType;
            }
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        //Display Grid
        GUILayout.Space(10);
        GUILayout.Label("Grid:", EditorStyles.boldLabel);

        // Reverse Y to follow the ingame spawn logic
        for (int y = currentLevel.Height - 1; y >= 0; y--)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            for (int x = 0; x < currentLevel.Width; x++)
            {
                var itemType = currentLevel.Tiles[x][y];

                if(!currentLevel.ActiveTypes.Contains(itemType))
                    currentLevel.Tiles[x][y] = itemType = ItemType.None;

                var texture = availableItems[itemType].Sprite.texture;
                if (GUILayout.Button(texture, GUILayout.Width(50), GUILayout.Height(50)))
                {
                    currentLevel.Tiles[x][y] = selectedItemType;
                }
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(10);
        if (GUILayout.Button("Clear Grid"))
        {
            ClearGrid();
        }

        GUILayout.Space(10);
        // Save Button
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
