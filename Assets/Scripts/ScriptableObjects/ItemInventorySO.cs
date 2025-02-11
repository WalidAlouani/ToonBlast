using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemInventory", menuName = "ScriptableObjects/ItemInventory", order = 4)]
public class ItemInventorySO : ScriptableObject
{
    public List<ItemTypeSO> Items;
    public Dictionary<ItemType, ItemTypeSO> ItemsByTypes;

    public ItemTypeSO Random;

    private void Awake()
    {
        PopulateDictionary();
    }

    private void OnValidate()
    {
        PopulateDictionary();
    }

    private void PopulateDictionary()
    {
        if (ItemsByTypes == null)
        {
            ItemsByTypes = new Dictionary<ItemType, ItemTypeSO>();
        }
        else
        {
            ItemsByTypes.Clear(); // Clear existing entries to prevent duplicates
        }

        if (Items == null)
            return;

        foreach (ItemTypeSO item in Items)
        {
            if (item == null)
            {
                Debug.LogWarning("Null ItemTypeSO found in ItemInventorySO list.");
                continue;
            }
            else if(ItemsByTypes.ContainsKey(item.Type))
            {
                Debug.LogWarning($"Duplicate key {item.Type} found in ItemInventorySO list.  Ignoring.");
                continue;
            }

            ItemsByTypes[item.Type] = item;
        }
    }

    public ItemTypeSO GetItemByType(ItemType type)
    {
        if (type == ItemType.None)
            return Random;

        if (ItemsByTypes == null || !ItemsByTypes.ContainsKey(type))
            return null;

        return ItemsByTypes[type];
    }
}
