using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_PopupAnimation : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float duration;
    [SerializeField] private Ease ease;

    private void OnEnable()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.DOFade(1, duration);
        rectTransform.anchoredPosition = Vector3.up * -50;
        rectTransform.DOAnchorPosY(0, duration).SetEase(ease);
    }
}
