using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class NextScene_02 : MonoBehaviour
{
    public Image _backgroundNextScene;
    public TextMeshProUGUI _textNextScene;
    public TextMeshPro _textAnswer;
    private bool isNextScene;
    private bool isAnswer;
    private bool isWriting;
    private AudioSource[] _useButtonEffect;

    private void Start()
    {
        Debug.Log("Start(1/3)");
        if (_backgroundNextScene != null && _textNextScene != null)
        {
            Debug.Log("Start(2/3)");
        }
        _textNextScene.enabled = false;
        _backgroundNextScene.enabled = false;
        isNextScene = false;
        isAnswer = false;
        isWriting = false;
        _textAnswer.enabled = false;

        _useButtonEffect = this.GetComponents<AudioSource>();
        Debug.Log("Start(3/3)");
    }

    void Update()
    {
        if (isWriting)
        {
            keyboardEntry();
            if (_textAnswer.text.Length > 3)
            {
                _textAnswer.text = "XXX";
                _useButtonEffect[1].Play();
            }
        }
        if (isNextScene && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)))
        {
            
            if (isWriting)
            {
                if (_textAnswer.text == "943")
                {
                    isAnswer = true;
                }
                else
                {
                    _textAnswer.text = "XXX";
                    _useButtonEffect[1].Play();
                }
                isWriting = false;
            }
            else
            {
                isWriting = true;
                _textAnswer.text = "";
            }
        }

        if (isAnswer)
        {
            _textNextScene.enabled = true;
            _backgroundNextScene.enabled = true;
            if (isNextScene && Input.GetKeyDown(KeyCode.E))
            {
                _useButtonEffect[0].Play();
                if(BackgroundMusic.Instance != null){ Destroy(BackgroundMusic.Instance.gameObject); }
                nextScene();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAnswer)
        {
            _textNextScene.enabled = true;
            _backgroundNextScene.enabled = true;
        }
        isNextScene = true;
        _textAnswer.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _textNextScene.enabled = false;
        _backgroundNextScene.enabled = false;
        isNextScene = false;
        _textAnswer.enabled = false;
        isWriting = false;
    }

    private void nextScene()
    {
        Debug.Log("nextScene");
        SceneManager.LoadScene("Scenes/Map_Lab/Map_03");
    }

    private void keyboardEntry()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            _textAnswer.text += "0";
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            _textAnswer.text += "1";
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            _textAnswer.text += "2";
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            _textAnswer.text += "3";
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            _textAnswer.text += "4";
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            _textAnswer.text += "5";
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            _textAnswer.text += "6";
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            _textAnswer.text += "7";
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            _textAnswer.text += "8";
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            _textAnswer.text += "9";
        }
    }
}
