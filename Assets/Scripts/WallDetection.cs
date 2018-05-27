using UnityEngine;
using System.Collections;

public class WallDetection : MonoBehaviour
{
    public Vector3 standing;          // where the checker will be when standing
    public Vector3 crouching;         // where the checker will be when crouching

    public Vector3 standingFlip;          // where the checker will be when standing
    public Vector3 crouchingFlip;         // where the checker will be when crouching
    private float crouch;
    public Transform ceilingCheck;
    private bool ceiled;                    //Check if ceiling is above player
    public bool facingRight = true;
    public float h;

    public Transform player;       // Reference to the player.
    private GameObject[] transforms;


    void Awake()
    {
        // Setting up the reference.
        transforms = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
       // for(int i =0;i < transforms.Length; i++)
       // {
       //     print(transforms[i].tag + transforms[i].transform.position + transforms[i].transform.name);
       // }

        //Check if ceiling is above player
        ceiled = Physics2D.Linecast(player.position, ceilingCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        // Set the position to the player's position with the offset.
        crouch = Input.GetAxisRaw("Crouch");
        if (crouch != 0 || ceiled == true)
        {
            if (facingRight)
            {
                transform.position = player.position + crouching;
            }
            else
            {
                transform.position = player.position + crouchingFlip;
            }
        }
        else
        {
            if (facingRight)
            {
                transform.position = player.position + standing;
            }
            else
            {
                transform.position = player.position + standingFlip;
            }
        }
    }

    private void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");

        // If the input is moving the player right and the player is facing left...
        if (h > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && facingRight)
            // ... flip the player.
            Flip();
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
