using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
    public void Quit()
    {
        Debug.Log("Player Quit The Game");
        Application.Quit();
    }
}
