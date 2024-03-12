using System.Collections;
using UnityEngine;

class GlobalAudioController : MonoBehaviour
{
    private AudioSource _audioSource;

    public static GlobalAudioController Instance;

    private void Awake()
    {
        Instance = this;
        _audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void PlayAudio(AudioClip clip, AudioSource source)
    {
        source.clip = clip;
        source.Play();
    }
}