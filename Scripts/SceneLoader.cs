using System;
using System.Collections;
using System.Collections.Generic;
using Player_Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    

    public void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void LoadTemple()
    {
        SceneManager.LoadScene("Temple");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu" || SceneManager.GetActiveScene().name == "Game Over")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}