﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeEnemy : MonoBehaviour {

	public float range;
	public GameObject Player;
	public Rigidbody2D myRigid;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
		if (Vector3.Distance(Player.transform.position, this.transform.position) < range){
			myRigid.velocity = new Vector2(Player.transform.position.x - this.transform.position.x , Player.transform.position.y - this.transform.position.y);
		} else {
			myRigid.velocity = new Vector2(0 , 0);
		}
	}
}