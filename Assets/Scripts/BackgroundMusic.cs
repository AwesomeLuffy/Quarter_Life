using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance = null;
    public static BackgroundMusic Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public static void PauseUnpause(){
        if (instance == null) {
            return;
        }

        if (instance.gameObject.GetComponent<AudioSource>().isPlaying) {
            instance.gameObject.GetComponent<AudioSource>().Pause();
            return;
        }
        instance.gameObject.GetComponent<AudioSource>().UnPause();
    }
}
