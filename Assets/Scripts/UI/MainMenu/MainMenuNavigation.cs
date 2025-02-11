using System;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuNavigation : MonoBehaviour
{
    [SerializeField] private List<MainMenuView> mainMenuScreens;

    private Dictionary<Type, MainMenuView> screenDictionary = new Dictionary<Type, MainMenuView>();

    private void Awake()
    {
        InitializeDictionary();
    }

    private void Start()
    {
        Show<MainScreen>();
    }

    private void InitializeDictionary()
    {
        foreach (var view in mainMenuScreens)
        {
            screenDictionary[view.GetType()] = view;
            view.Hide(); // Start with all hidden
        }
    }

    public void Show<T>() where T : MainMenuView
    {
        HideAll();
        if (screenDictionary.TryGetValue(typeof(T), out var view))
        {
            view.Show();
        }
    }

    public void HideAll()
    {
        foreach (var view in screenDictionary.Values)
        {
            view.Hide();
        }
    }
}
