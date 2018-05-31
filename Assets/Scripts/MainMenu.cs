using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenuUI;
    public GameObject instructions;
    public GameObject optionsMenu;
    public GameObject hero;
    public GameObject music;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1f;
        hero.SetActive(true);
        instructions.SetActive(false);
        optionsMenu.SetActive(false);   //Exit options menu if active
        mainMenuUI.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        SceneManager.LoadScene("Level");
    }

    public void Instructions()
    {
        mainMenuUI.SetActive(false);
        hero.SetActive(false);
        instructions.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Options()
    {
        mainMenuUI.SetActive(false);
        hero.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void Back()
    {
        Time.timeScale = 1f;
        hero.SetActive(true);
        instructions.SetActive(false);
        optionsMenu.SetActive(false);   //Exit options menu if active
        mainMenuUI.SetActive(true);
    }

    public void ToggleMusic()
    {
        if (music.activeInHierarchy == true)
        {
            music.SetActive(false);
        }
        else
        {
            music.SetActive(true);
        }
    }
}
