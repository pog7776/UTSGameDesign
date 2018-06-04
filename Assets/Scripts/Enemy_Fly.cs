using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Fly : MonoBehaviour {

    private Animator anim;			    // Reference to the animator component.
    public float moveSpeed = 2f;        // The speed the enemy moves at.
    public int HP = 2;                  // How many times the enemy can be hit before it dies.
    private bool dead = false;          // Whether or not the enemy is dead.

    public float timerTime = 10;
    public float timer = 10;            //time until door closes
    public bool count = false;          //begin counting down timer

    public GameObject fly;
    public GameObject startPos;
    public GameObject endPos;
    private Vector3 originalPos;
    private bool posReturn;

    private Quaternion targetRotation;

    // Use this for initialization
    void Start () {
        timer = timerTime;
        fly.transform.position = new Vector3(fly.transform.position.x, fly.transform.position.y, fly.transform.position.z);
        count = true;
        originalPos = new Vector3(fly.transform.position.y, fly.transform.position.y, 0);
    }
	
	// Update is called once per frame
	void Update () {
        //start timer if count == ture
        if (count == true)
        {
            timer = timer - Time.deltaTime;
        }

        float step = moveSpeed * Time.deltaTime;

        //move from points
        if (timer >= 0)
        {
            fly.transform.position = Vector3.MoveTowards(fly.transform.position, startPos.transform.position, step);
        }
        else
        {
            fly.transform.position = Vector3.MoveTowards(fly.transform.position, endPos.transform.position, step);
        }

        if(posReturn == true && fly.transform.position != originalPos)
        {
            fly.transform.position = Vector3.MoveTowards(fly.transform.position, startPos.transform.position, step);

            if (fly.transform.position == originalPos)
            {
                posReturn = false;
            }
        }

        //timer to move
        if (timer < -timerTime)
        {
            timer = timerTime;
        }

        //rotate back to up if not up
        if(fly.transform.rotation.z != 0)
        {
            //targetRotation *= Quaternion.AngleAxis(60, Vector3.up);
        }
        
        //fly.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * step);
    }

    private void FixedUpdate()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Hurt()
    {
        // Reduce the number of hit points by one.
        HP--;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // If the player hits the trigger...
        if (col.gameObject.tag == "Player")
        {
            posReturn = true;
        }

        // If the player hits the trigger...
        if (col.gameObject.tag == "Bullet" || col.gameObject.tag == "Explosion")
        {
            //Hurt();
            //timer = -timer;
            posReturn = true;
        }
    }
}
