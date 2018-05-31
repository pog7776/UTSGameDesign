using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{

    private Animator anim;					// Reference to the player's animator component.
    private bool climbing;

    private bool facingRight;

    public float speed = 2f;
    public float jumpX = 50f;
    public float jumpY = 50f;

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

        //if (GameObject.Find("Player").GetComponent <PlayerControl>().facingRight == true)     i want this for reference, if you remove it please tell me - jack


        if (Input.GetKeyDown(KeyCode.D)){
            facingRight = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            facingRight = false;
        }

        if (facingRight == true)
        {
            jumpX = 50f;
        }
        else if (facingRight == false)
        {
            jumpX = -50f;
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
                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(jumpX, jumpY));
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
