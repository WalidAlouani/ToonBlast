using DG.Tweening;
using UnityEngine;

public class TileAnimation : MonoBehaviour
{
    [SerializeField] private Ease animationType = Ease.InCubic;

    public void MoveToPosition(Vector2 position, float duration)
    {
        transform.DOLocalMove(position, duration).SetEase(animationType);
    }
}
