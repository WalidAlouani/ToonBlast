using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button button;

    public void Init(string levelNumber, bool isClickable, UnityAction onClick)
    {
        text.text = levelNumber;
        button.interactable = isClickable;
        button.onClick.AddListener(onClick);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
