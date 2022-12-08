using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityTutorial.PlayerControl;

public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu instance;
    public AudioMixer audioMixer;
    public bool _yInversion;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetMouseInversion(bool isMouseInverted)
    {
        _yInversion = isMouseInverted;
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
