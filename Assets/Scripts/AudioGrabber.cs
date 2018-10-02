using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGrabber : MonoBehaviour {

    public AudioSource audioSource;
    public string defaultAudioKey;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    #region PUBLIC METHODS

    public void MakeLocalAudioCall(string soundKey)
    {
        AudioEventArgs e = AudioManager.BuildAudioArgs(null, soundKey, audioSource);
        OnAudioGrabCall(e);
    }

    public void MakeAudioCall(string soundKey)
    {
        AudioEventArgs e = AudioManager.BuildAudioArgs(null, soundKey);
        OnAudioGrabCall(e);
    }

    public void DefaultAudioCall()
    {
        MakeAudioCall(defaultAudioKey);
    }

    public void DefaultLocalAudioCall()
    {
        MakeLocalAudioCall(defaultAudioKey);
    }

    #endregion

    #region EVENTS
    public static event AudioEventHandler AudioGrabCall;

    private void OnAudioGrabCall(AudioEventArgs e)
    {
        if(AudioGrabCall != null)
        {
            AudioGrabCall(this, e);
        }
    }
    #endregion
}
