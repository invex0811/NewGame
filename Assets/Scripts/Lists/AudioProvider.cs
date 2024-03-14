using UnityEngine;

static class AudioProvider
{
    public static AudioClip GetSound(Sound sound)
    {
        return sound switch
        {
            Sound.ButtonClick => Resources.Load<AudioClip>("Audio/ButtonClick"),
            Sound.PlayerFootstep1 => Resources.Load<AudioClip>("Audio/PlayerFootstep1"),
            Sound.PlayerFootstep2 => Resources.Load<AudioClip>("Audio/PlayerFootstep2"),
            Sound.PlayerFootstep3 => Resources.Load<AudioClip>("Audio/PlayerFootstep3"),
            Sound.PlayerFootstep4 => Resources.Load<AudioClip>("Audio/PlayerFootstep4"),
            Sound.DoorOpening => Resources.Load<AudioClip>("Audio/DoorOpening"),
            _ => null,
        };
    }
}

public enum Sound
{
    ButtonClick,
    PlayerFootstep1,
    PlayerFootstep2,
    PlayerFootstep3,
    PlayerFootstep4,
    DoorOpening,
}