using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject pauseMenuPanel;
    public GameObject optionsMenu;
    public GameObject music;


    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused && optionsMenu.activeInHierarchy == false)
            {
                Resume();
            }
            else if (optionsMenu.activeInHierarchy == true)
            {
                Back();
            }
            else
            {
                Pause();
            } 
        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuPanel.SetActive(true);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        Debug.Log("Loading Menu...");
        SceneManager.LoadScene("Menu");     //need to create variable for menu instead of hard coding it
        GameIsPaused = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void Options()
    {
        pauseMenuUI.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Back()
    {
        optionsMenu.SetActive(false);   //Exit options menu if active
        pauseMenuUI.SetActive(true);
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ToggleMusic()
    {
        if(music.activeInHierarchy == true)
        {
            music.SetActive(false);
        }
        else
        {
            music.SetActive(true);
        }
    }
}
