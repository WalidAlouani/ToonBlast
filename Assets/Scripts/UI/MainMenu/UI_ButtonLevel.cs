using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_ButtonLevel : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button button;
    [SerializeField] private Image lockIcon;
    [SerializeField] private Sprite star;
    [SerializeField] private Sprite emptyStar;
    [SerializeField] private Image[] stars;

    public void Init(string levelNumber, bool isUnlocked, UnityAction onClick)
    {
        text.text = levelNumber;
        button.interactable = isUnlocked;
        lockIcon.gameObject.SetActive(!isUnlocked);
        button.onClick.AddListener(onClick);

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].sprite = isUnlocked ? star : emptyStar;
        }
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
