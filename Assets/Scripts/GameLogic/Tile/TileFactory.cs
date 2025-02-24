using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    [SerializeField] private ItemInventorySO itemsInventory;
    [SerializeField] private Transform container;
    [SerializeField] private TileItem tilePrefab;

    private List<ItemType> spawnableTypes;
    private IPoolingSystem<TileItem> tilePool;

    public void Init(List<ItemType> activeTypes)
    {
        spawnableTypes = activeTypes
            .Where(el =>
            {
                return itemsInventory.GetItemByType(el).CanFall 
                    && itemsInventory.GetItemByType(el).IsDestroyable;
            })
            .ToList(); //ToDo: make the user chose what types are spawnable
        tilePool = new ObjectPool<TileItem>(tilePrefab, container);
    }

    public TileItem CreateTile(int x, int y, ItemType tileType)
    {
        TileItem tile = tilePool.Get();

        tile.OnDestroy += OnTileDestroyed;

        if (tileType == ItemType.None)
            tileType = spawnableTypes[Random.Range(0, spawnableTypes.Count)];

        var typeData = itemsInventory.GetItemByType(tileType);
        tile.Init(x, y, typeData);
        return tile;
    }

    private void OnTileDestroyed(TileItem tile)
    {
        tile.OnDestroy -= OnTileDestroyed;
        tilePool.Return(tile);
    }
}
