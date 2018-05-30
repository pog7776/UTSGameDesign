using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder : MonoBehaviour
{

    public float speed = 2f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
            else
            {
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.60f);
            }
        }



    }
}
