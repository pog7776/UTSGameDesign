﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoints : MonoBehaviour {

    private GameObject player;
    public Vector3 checkPointPos;
    public bool isTriggered;
    public float health;
    public Vector3 camPos;
    private string sceneName;

    public CheckPointController checkPointController;

	// Use this for initialization
	void Start () {
        checkPointPos = new Vector3(0, 0, 0);
        isTriggered = false;
        checkPointController = FindObjectOfType<CheckPointController>();
        sceneName = SceneManager.GetActiveScene().name;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            checkPointPos = other.transform.position;
            checkPointController.SetAllCheckPointsToFalse();
            isTriggered = true;
            player = other.GetComponent<GameObject>();
            health = FindObjectOfType<PlayerHealth>().health;
            if(sceneName.Equals("Level4"))
            {
                camPos = FindObjectOfType<Camera>().transform.position;
            }
            


            //if (health < 50)
            //{ health = 50; }
        }
    }
}
