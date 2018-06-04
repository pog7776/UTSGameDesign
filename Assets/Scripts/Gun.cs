using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public Rigidbody2D rocket;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.
    public bool unPaused = true;            //make sure the game is unpaused
    
    private PlayerControl playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.

	void Awake()
	{
		// Setting up the references.
		anim = transform.root.gameObject.GetComponent<Animator>();
		playerCtrl = transform.root.GetComponent<PlayerControl>();
	}


    void Update ()
	{

        //...setting shoot direction
        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;

        // If the fire button is pressed...
        if (Input.GetButtonDown("Fire1") && PauseMenu.GameIsPaused == false)
		{
			// ... set the animator Shoot trigger parameter and play the audioclip.
			anim.SetTrigger("Shoot");
			GetComponent<AudioSource>().Play();

            Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
            bulletInstance.velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * speed;

            //non mouse control
            // If the player is facing right...
            //if (playerCtrl.facingRight)
			//{
				// ... instantiate the rocket facing right and set it's velocity to the right. 
				//Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				//bulletInstance.velocity = new Vector2(shootDirection.x * speed, shootDirection.y * speed);
			//}
			//else
			//{
				// Otherwise instantiate the rocket facing left and set it's velocity to the left.
				//Rigidbody2D bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
				//bulletInstance.velocity = new Vector2(-speed, 0);
			//}
		}
    }
}
