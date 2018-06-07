using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySummoner : MonoBehaviour {
    public int HP = 2;
    public float range;
    public float timer;
    public GameObject Player;
    public Rigidbody2D myRigid;
    public Rigidbody2D enemy;
    public bool active = false;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(Player.transform.position, this.transform.position) < range && !active)
        {
            active = true;
            Invoke("Spawn", timer);
        }

        if (active)
        {
            if (Vector3.Distance(Player.transform.position, this.transform.position) > range/2)
            {
                myRigid.AddForce(Player.transform.position - transform.position);
            } else
            {
                myRigid.AddForce(- Player.transform.position + transform.position);
            }
        }
    }

    void Spawn ()
    {
        Rigidbody2D bulletInstance = Instantiate(enemy, this.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Invoke("Spawn", timer);
    }

}
