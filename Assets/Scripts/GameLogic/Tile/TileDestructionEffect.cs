using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

// Change to scriptable object
public class TileDestructionEffect : MonoBehaviour
{
    [SerializeField] private TileFactory tileFactory;
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private Ease ease;
    [SerializeField] private ItemType[] supportedTypes;

    private void OnEnable()
    {
        ServiceLocator.Get<EventManager>().OnTilesDestroyed.Subscribe(OnTilesDestroyed, this);
    }

    private void OnDisable()
    {
        if (ServiceLocator.TryGet<EventManager>(out var eventManager))
            eventManager.OnTilesDestroyed.Unsubscribe(OnTilesDestroyed);
    }

    private void OnTilesDestroyed(List<TileItem> destroyedTiles)
    {
        if (destroyedTiles.Count <= 0)
            return;

        var clickedTile = destroyedTiles[0];
        var clickedTileType = clickedTile.ItemType;
        
        // we only play this anination for the supported types
        if (!supportedTypes.Contains(clickedTileType))
            return;

        var sameTypeItems = destroyedTiles.Where(el => el.ItemType == clickedTileType).ToList();

        if (sameTypeItems.Count > 5)
        {
            var targetPosition = new Vector3(clickedTile.X, clickedTile.Y, -1);

            foreach (TileItem item in sameTypeItems)
            {
                var newTile = tileFactory.CreateTile(item.X, item.Y, item.ItemType);

                var tilePosition = new Vector3(item.X, item.Y, -1);
                var direction = tilePosition - targetPosition;
                var firstDestination = targetPosition + direction * 1.2f;

                newTile.transform
                    .DOLocalMove(firstDestination, duration * 0.35f)
                    .SetEase(ease)
                    .OnComplete(() =>
                        newTile.transform
                        .DOLocalMove(targetPosition, duration * 0.65f)
                        .SetEase(ease)
                        .OnComplete(() => newTile.Destroy()));
            }
        }
        else
        {
            foreach (TileItem item in sameTypeItems)
            {
                var newTile = tileFactory.CreateTile(item.X, item.Y, item.ItemType);

                newTile.transform
                    .DOScale(1.3f, duration * 0.15f)
                    .SetEase(ease)
                    .OnComplete(() =>
                        newTile.transform
                        .DOScale(0.2f, duration * 0.4f)
                        .SetEase(ease)
                        .OnComplete(() => newTile.Destroy()));
            }
        }
    }
}
