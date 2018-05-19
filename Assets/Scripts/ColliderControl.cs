using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderControl : MonoBehaviour {

    public BoxCollider2D stand;
    public BoxCollider2D crouch;
    public BoxCollider2D faceSpace;
    public CircleCollider2D circle;

    private Animator anim;

    PlayerControl playerC;
	// Use this for initialization
	void Start () {
        playerC = GetComponent<PlayerControl>();
        stand.enabled = true;
        crouch.enabled = true;
        faceSpace.enabled = true;
        circle.enabled = true;

	}
	
	// Update is called once per frame
	void Update () {
        if (playerC.grounded == false)
        {
            stand.enabled = true;
            faceSpace.enabled = true;
            crouch.enabled = false;
            circle.enabled = false;
        }
        else
        {
            if (playerC.crouching == true)
            {
                stand.enabled = false;
                faceSpace.enabled = false;
                crouch.enabled = true;
                circle.enabled = true;
            }
            else
            {
                stand.enabled = true;
                faceSpace.enabled = true;
                crouch.enabled = false;
                circle.enabled = true;
            }
        }

        if(crouch.enabled == true)
        {
            playerC.playerSpeed = playerC.crouchSpeed;
            playerC.jumpForce = playerC.crouchJump;
            //anim.SetFloat("Speed", 0.5f);
        }
        else
        {
            playerC.playerSpeed = playerC.originalSpeed;
            playerC.jumpForce = playerC.originalJump;
        }

	}
}
