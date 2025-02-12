using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LevelCompletePopup : MonoBehaviour
{
    [SerializeField] private LevelDataManager levelDataManager;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button nextLevelButton;

    private void Start()
    {
        closeButton.onClick.AddListener(() => SceneLoader.Instance.LoadMenuScene());
        nextLevelButton.onClick.AddListener(() =>
        {
            levelDataManager.SelectNextLevel();
            SceneLoader.Instance.LoadGameScene();
        });
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveAllListeners();
        nextLevelButton.onClick.RemoveAllListeners();
    }
}
