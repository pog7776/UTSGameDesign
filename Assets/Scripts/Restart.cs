using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Restart : MonoBehaviour
{

    Scene currentScene;

    private float restartGame;
    private PlayerControl playerControl;

    public void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
    }

    void Update()
    {
        restartGame = Input.GetAxisRaw("Restart");

        currentScene = SceneManager.GetActiveScene();




        if (restartGame != 0){
            CheckPointController checkPoint = FindObjectOfType<CheckPointController>();
            CheckPoints check = checkPoint.GetLastCheckPoint();

            playerControl.SetPosition(check.checkPointPos);
            float currHealth = playerControl.GetComponent<PlayerHealth>().health;
            if(currHealth < 50)
            {
                playerControl.GetComponent<PlayerHealth>().health = 50;
            }

            if (currentScene.name =="Level4")
            {
                FindObjectOfType<Camera>().transform.position = check.camPos;
            }

        }
    }
}