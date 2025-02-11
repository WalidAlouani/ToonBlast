using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_GoalElement : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text value;

    public void Init(ItemTypeSO itemData, int count)
    {
        icon.sprite = itemData.Sprite;
        value.text = count.ToString();
    }

    public void UpdateValue(int count) 
    {
        value.text = count.ToString();
    }
}
