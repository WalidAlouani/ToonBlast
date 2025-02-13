using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TileDestructionEffect : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private Ease ease;

    private void Start()
    {
        boardManager.OnTilesDestroyed += OnTilesDestroyed;
    }

    private void OnDestroy()
    {
        boardManager.OnTilesDestroyed -= OnTilesDestroyed;
    }

    private void OnTilesDestroyed(Vector2Int position, List<TileItem> list)
    {
        if (list.Count > 5)
        {
            var targetPosition = new Vector3(position.x, position.y, -1);

            foreach (TileItem item in list)
            {
                var newTile = boardManager.CreateTile(item.X, item.Y, item.ItemType);

                var tilePosition = new Vector3(item.X, item.Y, -1);
                var direction = tilePosition - targetPosition;
                var firstDestination = targetPosition + direction * 1.2f;
                var finalDestination = new Vector3(position.x, position.y, -1);

                newTile.transform
                    .DOLocalMove(firstDestination, duration * 0.35f)
                    .SetEase(ease)
                    .OnComplete(() =>
                        newTile.transform
                        .DOLocalMove(finalDestination, duration * 0.65f)
                        .SetEase(ease)
                        .OnComplete(() =>  newTile.Destroy()));
            }
        }
        else
        {
            foreach (TileItem item in list)
            {
                var newTile = boardManager.CreateTile(item.X, item.Y, item.ItemType);

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
