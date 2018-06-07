using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Restart : MonoBehaviour
{

    private float restartGame;
    private PlayerControl playerControl;

    public void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
    }

    void Update()
    {
        restartGame = Input.GetAxisRaw("Restart");

        


        if(restartGame != 0){
            CheckPointController checkPoint = FindObjectOfType<CheckPointController>();
            CheckPoints check = checkPoint.GetLastCheckPoint();

            playerControl.SetPosition(check.checkPointPos);

        }
    }
}