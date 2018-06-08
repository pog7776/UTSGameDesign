using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    private string sceneName;
    public static string lastLevel;
    Scene currentScene;
    public int level;

    public string previousLevel;

    // Use this for initialization
    void Start()
    {
       currentScene = SceneManager.GetActiveScene();
        
        sceneName = currentScene.name;


        if(sceneName == "level99")
        {
            lastLevel = "level";
        }

        if (sceneName == "level3")
        {
            lastLevel = "level99";
        }

        if (sceneName == "level4")
        {
            lastLevel = "level3";
        }
        //ignore this stuff, became obsolete... using it for reference
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScene.name != "ControlRoom") //currentScene.name != "Menu" || 
        {
            lastLevel = currentScene.name;
        }

        previousLevel = lastLevel;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // If the player hits the trigger...
        if (col.gameObject.tag == "Player")
        {

            if (level == 0 || lastLevel == "level5")
            {
                SceneManager.LoadScene("Menu");
            }

            if (level == 1)
            {
                SceneManager.LoadScene("ControlRoom");
            }

            if (level == 2)
            {
                SceneManager.LoadScene("Level");
            }

            if (previousLevel == "Level")
            {
                SceneManager.LoadScene("Level99");
            }

            if (lastLevel == "Level99")
            {
                SceneManager.LoadScene("Level3");
            }

            if (lastLevel == "Level3")
            {
                SceneManager.LoadScene("Level4");
            }

            if (lastLevel == "Level4")
            {
                SceneManager.LoadScene("Level5");
            }

            if (lastLevel == "Level5")
            {
                SceneManager.LoadScene("Menu");
            }

        }

    }
}
