using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundBank", menuName = "ScriptableObjects/SoundBank", order = 5)]
public class SoundBankSO : ScriptableObject
{
    public SoundTriggerPair[] Sounds;
    public Dictionary<SoundTrigger, AudioClip> SoundsByTypes;

    private void Awake()
    {
        PopulateDictionary();
    }

    private void OnValidate()
    {
        PopulateDictionary();
    }

    private void PopulateDictionary()
    {
        if (SoundsByTypes == null)
        {
            SoundsByTypes = new Dictionary<SoundTrigger, AudioClip>();
        }
        else
        {
            SoundsByTypes.Clear(); // Clear existing entries to prevent duplicates
        }

        if (Sounds == null)
            return;

        foreach (var sound in Sounds)
        {
            if (sound == null)
            {
                Debug.LogWarning("Null SoundPairTrigger found in SoundBank list.");
                continue;
            }
            else if (SoundsByTypes.ContainsKey(sound.SoundTrigger))
            {
                Debug.LogWarning($"Duplicate key {sound.SoundTrigger} found in SoundBank list. Ignoring.");
                continue;
            }

            SoundsByTypes[sound.SoundTrigger] = sound.AudioClip;
        }
    }

    public AudioClip GetSoundByTrigger(SoundTrigger trigger)
    {
        if (SoundsByTypes == null || !SoundsByTypes.ContainsKey(trigger))
            return null;

        return SoundsByTypes[trigger];
    }
}
