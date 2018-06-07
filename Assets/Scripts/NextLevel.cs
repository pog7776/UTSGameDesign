using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    private string sceneName;
    private string lastLevel;
    Scene currentScene;
    public int level;

    // Use this for initialization
    void Start()
    {
       currentScene = SceneManager.GetActiveScene();

        //if (currentScene.name == m_MyFirstScene)

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

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // If the player hits the trigger...
        if (col.gameObject.tag == "Player")
        {

            if (level == 0)
            {
                SceneManager.LoadScene("Menu");
            }

            if (level == 1)
            {
                SceneManager.LoadScene("ControlRoom");
            }

            if (level == 2)
            {
                SceneManager.LoadScene("level");
            }

            if (level == 3)
            {
                SceneManager.LoadScene("level99");
            }

            if (level == 4)
            {
                SceneManager.LoadScene("level3");
            }

            if (level == 4)
            {
                SceneManager.LoadScene("level4");
            }

        }

    }
}
