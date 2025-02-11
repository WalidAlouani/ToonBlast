using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class LevelEditorWindow : EditorWindow
{
    [SerializeField] private LevelEditorSO config;

    private LevelEditorController controller;

    private Dictionary<LevelEditorScreen, ILevelEditorView> viewMapping;

    private LevelEditorScreen currentView = LevelEditorScreen.LevelList;

    [MenuItem("Tools/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditorWindow>("Level Editor");
    }

    private void OnEnable()
    {
        controller = new LevelEditorController(config);

        viewMapping = new Dictionary<LevelEditorScreen, ILevelEditorView>()
        {
            { LevelEditorScreen.LevelList, new LevelListView(controller, ChangeView) },
            { LevelEditorScreen.LevelEditor, new LevelEditorView(controller, ChangeView) },
            { LevelEditorScreen.CreateLevel, new CreateLevelView(controller, ChangeView) },
        };
    }

    private void OnGUI()
    {
        if (config == null)
        {
            GUILayout.Label("No config found! Please create a config file.");
            return;
        }

        viewMapping[currentView].OnRender();
    }

    public void ChangeView(LevelEditorScreen view)
    {
        viewMapping[currentView].OnExit();
        currentView = view;
        viewMapping[currentView].OnEnter();
    }
}
