using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    CursorLockMode desiredMode;
    private Canvas _canvas;

    private void Start()
    {
        _canvas = GameObject.Find("PauseMenu").GetComponent<Canvas>();
        _canvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        BackgroundMusic.PauseUnpause();
        _canvas.enabled = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        desiredMode = CursorLockMode.Confined;
    }
    private void Pause()
    {
        BackgroundMusic.PauseUnpause();
        _canvas.enabled = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        desiredMode = CursorLockMode.None;
        {
            Cursor.lockState = desiredMode;
        }
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
