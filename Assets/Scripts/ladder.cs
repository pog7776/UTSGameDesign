using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{

    private Animator anim;					// Reference to the player's animator component.
    public float speed = 2f;
    private bool climbing;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(climbing == true)
        {
            //anim.SetBool("Climbing", true);
        }
        else
        {
            //anim.SetBool("Climbing", false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.W))
            {
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 10f));
            }
            else
            {
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.60f);
            }

            climbing = true;

        }
        else
        {
            climbing = false;
        }


    }
}
