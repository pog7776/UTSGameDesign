using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public static int score = 0;
    public int visibleScore = 0;
    public Text scoreText;

    // Use this for initialization
    void Start () {
        score = 0;
        visibleScore = 0;     //for Debug
        SetScoreText();
	}
	
	// Update is called once per frame
	void Update () {

        if (Collectable.score > score)
        {
            score = Collectable.score;
            SetScoreText();
        }

        //for debug
        if(score == -1)
        {
            visibleScore = 0;
        }
        else
        {
            visibleScore = score;
        }
	}

    void SetScoreText()
    {
        scoreText.text = "Score: " + score;

    }
}
