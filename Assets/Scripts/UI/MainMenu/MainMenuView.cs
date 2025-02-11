using UnityEngine;

public abstract class MainMenuView : MonoBehaviour
{
    [SerializeField] protected MainMenuNavigation navigation;

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
