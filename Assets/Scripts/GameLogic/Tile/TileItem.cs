using System;
using UnityEngine;

public abstract class TileItem : MonoBehaviour, IBoardElement, IPoolable, IClickable, IDestroyable, IDamagable
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TileAnimation tileAnimation;

    public int X { get; private set; }
    public int Y { get; private set; }
    public bool CanFall => data.CanFall;
    public ItemType ItemType => data.Type;
    public IMatchingRule MatchingRule => data.MatchingRule;
    public TileDamageType DamageType => data.DamageType;
    public Action<TileItem> OnClick { get; set; }
    public Action<TileItem> OnDestroy { get; set; }
    public Action<TileItem> OnDamageReceived { get; set; }

    protected ItemTypeSO data;
    protected int health;

    public void Init(int x, int y, ItemTypeSO typeData)
    {
        SetCoordinates(x, y);
        SetPosition(x, y);
        UpdateVisual(typeData.GetSprite());
        data = typeData;
        health = data.Health;
        name = $"Tile[{x},{y}]";
    }

    public void SetCoordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void SetPosition(int x, int y, float animationDuration = 0)
    {
        if (animationDuration != 0)
            tileAnimation.MoveToPosition(new Vector2(x, y), animationDuration);
        else
            transform.localPosition = new Vector2(x, y);
    }

    public void UpdateVisual(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public virtual void Click()
    {
        if (!data.IsClickable)
            return;

        OnClick?.Invoke(this);
    }

    public virtual void Destroy()
    {
        if (!data.IsDestroyable)
            return;

        OnDestroy?.Invoke(this);
    }

    public virtual bool Damage()
    {
        health--;
        var destoy = health <= 0;

        if (!destoy)
        {
            var index = data.Health - health;
            var sprite = data.GetSprite(index);
            if (sprite != null)
                UpdateVisual(sprite);
        }
        else
        {
            Destroy();
        }

        return destoy;
    }

    public void OnCreate() { }

    public void OnRelease()
    {
        transform.localScale = Vector3.one;
        SetPosition(-1, -1);
    }
}

public interface IBehavior { }

public interface IClickable : IBehavior
{
    Action<TileItem> OnClick { get; set; }
    void Click();
}

public interface IDestroyable : IBehavior
{
    Action<TileItem> OnDestroy { get; set; }
    void Destroy();
}

public interface IDamagable : IBehavior
{
    Action<TileItem> OnDamageReceived { get; set; }
    bool Damage();
}

public interface IExplodable : IBehavior
{
    Action<TileItem> OnExplode { get; set; }
    bool Explode();
}

