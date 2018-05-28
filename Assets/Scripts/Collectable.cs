using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public static int score = 0;


    // Use this for initialization
    void Start () {
        score = Score.score;
	}

    // Update is called once per frame
    void Update () {

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            score++;
            gameObject.SetActive(false);
        }

    }
}
