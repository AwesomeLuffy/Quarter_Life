using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class NextScene : MonoBehaviour
{
    public Image _backgroundNextScene;
    public TextMeshProUGUI _textNextScene;
    private bool isNextScene;
    private AudioSource _useButtonSource;

    private void Start()
    {
        _textNextScene.enabled = false;
        _backgroundNextScene.enabled = false;
        isNextScene = false;
        _useButtonSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isNextScene && Input.GetKeyDown(KeyCode.E))
        {
            _useButtonSource.Play();
            nextScene();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _textNextScene.enabled = true;
        _backgroundNextScene.enabled = true;
        isNextScene = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _textNextScene.enabled = false;
        _backgroundNextScene.enabled = false;
        isNextScene = false;
    }

    private void nextScene()
    {
        // SceneManager.LoadScene("Scenes/Map_Lab/Map_02");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
