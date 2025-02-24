using UnityEngine;

[CreateAssetMenu(fileName = "ItemType", menuName = "ScriptableObjects/CreateItemType", order = 2)]
public class ItemTypeSO : ScriptableObject
{
    public ItemType Type;
    public string Name;
    public Sprite[] Sprites;
    public Color Color;
    public bool IsClickable;
    public bool IsDestroyable;
    public bool CanFall;
    public int Health;
    public TileDamageType DamageType;
    public MatchingRuleSO MatchingRule;

    public Sprite GetSprite(int index = 0)
    {
        if (index < 0 || index >= Sprites.Length)
            return null;

        return Sprites[index];
    }

    public Texture GetTexture(int index = 0)
    {
        if (index < 0 || index >= Sprites.Length)
            return null;

        return Sprites[index].texture;
    }
}
