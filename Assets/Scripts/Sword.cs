using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {
    public bool attacking = false;

    public BoxCollider2D attackTrigger;

    private float attackTimer = 0;
    private float attackCd = 0.3f;

    private PlayerControl playerCtrl;       // Reference to the PlayerControl script.
    private Animator anim;                  // Reference to the Animator component.

    void Awake()
    {
        // Setting up the references.
        anim = transform.root.gameObject.GetComponent<Animator>();
        playerCtrl = transform.root.GetComponent<PlayerControl>();
        attackTrigger.enabled = false;
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {


        // If the RMB button is pressed...
        if (Input.GetButtonDown("Fire2") && playerCtrl.crouching == false && !attacking)
        {
            // ... set the animator Shoot trigger parameter and play the audioclip.
            anim.SetTrigger("Slash");
            //GetComponent<AudioSource>().Play();

            attacking = true;
            attackTimer = attackCd;

            attackTrigger.enabled = true;      
            
        }

        if (attacking)
        {
            if(attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackTrigger.enabled = false;
            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Physics.IgnoreCollision(explosion.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}
