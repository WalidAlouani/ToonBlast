using UnityEngine;

[CreateAssetMenu(fileName = "ItemType", menuName = "ScriptableObjects/CreateItemType", order = 2)]
public class ItemTypeSO : ScriptableObject
{
    public ItemType Type;
    public string Name;
    public Sprite Sprite;
    public Color Color;
}
