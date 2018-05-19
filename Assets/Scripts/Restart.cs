using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Restart : MonoBehaviour
{

    private float restartGame;

    public void RestartGame()
    {

    }

    void Update()
    {
        restartGame = Input.GetAxisRaw("Restart");

        if(restartGame != 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
        }
    }
}