using DG.Tweening;
using UnityEngine;

public class UI_ScaleAnimation : MonoBehaviour
{
    [SerializeField] private float duration = 1;
    [SerializeField] private float startScale = 1;
    [SerializeField] private float endScale = 1;
    [SerializeField] private Ease ease;

    private void OnEnable()
    {
        transform.localScale = Vector3.one * startScale;
        transform.DOScale(endScale, duration).SetEase(ease);
    }
}
