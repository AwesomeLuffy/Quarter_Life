using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class TriggerPlatform : MonoBehaviour{
    private AudioSource _metalStep;
    void Start(){
        _metalStep = GetComponent<AudioSource>();
        Debug.Log("prout");
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log("test");
        _metalStep.Play();
    }

    private void OnTriggerStay(Collider other){
        Debug.Log("prout1");
    }

    private void OnTriggerExit(Collider other){
        Debug.Log("prout2");

    }
}
