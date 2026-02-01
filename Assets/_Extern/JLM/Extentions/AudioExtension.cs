using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AudioExtension
{
    public static AudioSource Play(AudioClip clip, float volume = 1, float pitch = 1)
    {
        pitch = Mathf.Clamp(pitch, 0, 3);
        volume = Mathf.Clamp(volume, 0, 1);

        if (!clip || pitch == 0)
            return null;
        
        AudioSource audioSource = new GameObject("PlayClip").AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.spatialBlend = 0;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
        Object.Destroy(audioSource.gameObject, clip.length / pitch);
        return audioSource;
    }
}


