using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChange : MonoBehaviour
{
    public GameObject c;
    public float speed;
    

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "MainCamera")
        {
            c.GetComponent<AutoScroll>().SetSpeed(0);
            Invoke("SetSpeed", 3);
        }
    }

    void SetSpeed()
    {
        c.GetComponent<AutoScroll>().SetSpeed(speed);
    }
}
