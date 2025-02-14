using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_SettingsPopup : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private UI_ToggleButton musicButton;
    [SerializeField] private UI_ToggleButton soundFXButton;

    void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound(SoundTrigger.CloseButton);
            gameObject.SetActive(false);
        });

        var isSoundMuted = AudioManager.Instance.IsMuted();
        soundFXButton.Init(isSoundMuted);
        soundFXButton.OnToggleChanged += AudioManager.Instance.Mute;

        // Music system not yet implemented. This is just ba fake Button
        musicButton.Init(true);
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveAllListeners();
    }
}
