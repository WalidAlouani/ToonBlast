using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_ToggleButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;

    public Action<bool> OnToggleChanged;
    private bool isOn;

    public void Init(bool isOn)
    {
        this.isOn = isOn;
        UpdateVisual(isOn);

        button.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(SoundTrigger.ClickButton);
            this.isOn = !this.isOn;
            UpdateVisual(this.isOn);
            OnToggleChanged?.Invoke(this.isOn);
        });
    }

    private void UpdateVisual(bool value)
    {
        image.gameObject.SetActive(value);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
