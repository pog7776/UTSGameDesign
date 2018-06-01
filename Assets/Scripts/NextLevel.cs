using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    public int level;

    // Use this for initialization
    void Start()
    {

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
                SceneManager.LoadScene("level");
            }

            if (level == 2)
            {
                SceneManager.LoadScene("level99");
            }

            if (level == 3)
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
