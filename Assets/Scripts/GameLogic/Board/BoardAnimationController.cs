using System;
using UnityEngine;

public class BoardAnimationController : MonoBehaviour
{
    [SerializeField] private float fallDuration = 0.3f;
    [SerializeField] private float rearrangeDuration = 0.3f;
    public float RefillAndFallDuration => Mathf.Max(rearrangeDuration, fallDuration);

    public void FallAnimation(TileItem tileItem, Vector2Int from, Vector2Int to)
    {
        tileItem.SetPosition(from.x, from.y);
        tileItem.SetPosition(to.x, to.y, fallDuration);
    }

    internal void RearrangeAnimation(TileItem tileItem, Vector2Int to)
    {
        tileItem.SetPosition(to.x, to.y, rearrangeDuration);
    }
}
