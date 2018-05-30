using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public static GameMaster gm;
    public GameObject AudioCoin;
    public GameObject AudioHealth;

    private float timer;


    private void Awake()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
            //gm = this;
        }
    }

    private void Update()
    {
        if(AudioCoin.activeInHierarchy == true)
        {

            AudioCoin.SetActive(false);

            timer = 0;
            timer = timer + Time.deltaTime;

            if(timer > 1)
            {
                //AudioCoin.SetActive(false);
            }
        }

        if (AudioHealth.activeInHierarchy == true)
        {

            AudioHealth.SetActive(false);

            timer = 0;
            timer = timer + Time.deltaTime;

            if (timer > 1)
            {
                AudioHealth.SetActive(false);
            }
        }
    }

}
