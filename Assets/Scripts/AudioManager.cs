using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void AudioEventHandler(object sender, AudioEventArgs e);

public struct AudioEventArgs
{
    public Sound sound;
    public string soundKey;
    public AudioSource audioSourceOverride;
}

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    [SerializeField]
    private Sound[] m_sounds;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        AudioGrabber.AudioGrabCall += AudioRequestReceived;
    }

    public void PlayAudio(string soundKey)
    {
        Sound soundToPlay = LocateSoundByKey(soundKey);

        soundToPlay.audioSource.volume = soundToPlay.volume;
        soundToPlay.audioSource.pitch = soundToPlay.pitch;
        soundToPlay.audioSource.loop = soundToPlay.isLooping;
        soundToPlay.audioSource.Play();
    }

    public void PlayAudioOneShot(string soundKey, AudioSource source)
    {
        Sound soundToPlay = LocateSoundByKey(soundKey);

        source.volume = soundToPlay.volume;
        source.pitch = soundToPlay.pitch;
        source.loop = soundToPlay.isLooping;
        source.Play();
    }

    public static AudioEventArgs BuildAudioArgs(Sound sound, string soundKey, AudioSource audioSource = null)
    {
        AudioEventArgs e;

        e.sound = sound;
        e.soundKey = soundKey;
        e.audioSourceOverride = audioSource;

        return e;
    }

    private void AudioRequestReceived(object sender, AudioEventArgs e)
    {
        if(e.audioSourceOverride != null)
        {
            PlayAudioOneShot(e.soundKey, e.audioSourceOverride);
        } else
        {
            PlayAudio(e.soundKey);
        }
    }

    private Sound LocateSoundByKey(string soundKey)
    {
        Sound targetSound = Array.Find<Sound>(m_sounds, sound => sound.name == soundKey);
        if(targetSound == null)
        {
            Debug.LogWarning("Requested sound '" + soundKey + "', but could not find it in audio manager.");
            return null;
        } else
        {
            return targetSound;
        }
    }
}
