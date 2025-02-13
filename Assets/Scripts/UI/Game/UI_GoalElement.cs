using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_GoalElement : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Image check;
    [SerializeField] private TMP_Text value;

    public void Init(ItemTypeSO itemData, int count)
    {
        icon.sprite = itemData.Sprite;
        UpdateValue(count);
    }

    public void UpdateValue(int count)
    {
        value.text = count.ToString();

        if (count == 0)
        {
            check.gameObject.SetActive(true);
            value.gameObject.SetActive(false);
        }
    }
}
