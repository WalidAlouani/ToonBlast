using DG.Tweening;
using UnityEngine;

public class TileItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float animationDuration = 0.35f;
    [SerializeField] private Ease animationType = Ease.InCubic;

    private ItemTypeSO data;
    public ItemType ItemType => data.Type;
    public int X { get; private set; }
    public int Y { get; private set; }

    public void Init(int x, int y, ItemTypeSO typeData)
    {
        X = x;
        Y = y;
        transform.localPosition = new Vector2(x, y);
        data = typeData;
        spriteRenderer.sprite = data.Sprite;
        name = $"Tile[{x},{y}]";
    }

    public void UpdateCoordinates(int x, int y, bool animate = true)
    {
        X = x;
        Y = y;

        if (animate)
            transform.DOLocalMove(new Vector2(x, y), animationDuration).SetEase(animationType);
        else
            transform.localPosition = new Vector2(x, y);
    }

    public void Tapped()
    {
        DestroyImmediate(gameObject);
    }
}
