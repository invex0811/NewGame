using UnityEngine;

static class GlobalAudioService
{
    public static void PlayAudio(AudioClip clip, AudioSource source)
    {
        source.clip = clip;
        source.Play();
    }
}