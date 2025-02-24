using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private SoundBankSO soundBank;
    [SerializeField] private AudioSource audioSource;
    private IPlayerDataManager playerData;

    private void Start()
    {
        playerData = ServiceLocator.Get<IPlayerDataManager>();
        var isSoundMuted = playerData.LoadIsSoundMuted();
        audioSource.mute = isSoundMuted;
    }

    public void PlaySound(SoundTrigger soundTrigger)
    {
        var soundClip = soundBank.GetSoundByTrigger(soundTrigger);
        if(soundClip == null)
            return;

        audioSource.PlayOneShot(soundClip);
    }

    public bool IsMuted()
    {
        return audioSource.mute;
    }

    public void Mute(bool isSoundMuted)
    {
        audioSource.mute = isSoundMuted;
        playerData.SaveIsSoundMuted(isSoundMuted);
    }
}
