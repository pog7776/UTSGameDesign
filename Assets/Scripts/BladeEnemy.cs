using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeEnemy : MonoBehaviour {

	public float range;
    public int HP = 1;
	public GameObject player;
	public Rigidbody2D myRigid;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
        if (player)
        {
            if (Vector3.Distance(player.transform.position, this.transform.position) < range)
            {
                myRigid.velocity = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
            }
            else
            {
                myRigid.velocity = new Vector2(0, 0);
            }
        }
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
}