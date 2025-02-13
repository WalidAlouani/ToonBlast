using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_ButtonLevel : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button button;
    [SerializeField] private Image lockIcon;

    public void Init(string levelNumber, bool isUnlocked, UnityAction onClick)
    {
        text.text = levelNumber;
        button.interactable = isUnlocked;
        lockIcon.gameObject.SetActive(!isUnlocked);
        button.onClick.AddListener(onClick);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
