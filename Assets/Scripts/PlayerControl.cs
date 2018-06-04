using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	//[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.


	public float moveForce = 100f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 1000f;         // Amount of force added when the player jumps.
    [HideInInspector]
    public AudioClip[] taunts;             // Array of clips for when the player taunts.
    [HideInInspector]
    public float tauntProbability = 50f;   // Chance of a taunt happening.
    [HideInInspector]
    public float tauntDelay = 1f;			// Delay for when the taunt should happen.

    public float playerSpeed;
    public float h;

    public float originalJump = 700;
    public float originalSpeed = 1;
    public float crouchJump = 350;
    public float crouchSpeed = 0.5f;

    private Transform player;
    public Transform ceilingCheck;
    public Transform wallCheck;
    [HideInInspector]
    public bool ceiled;                    //Check if ceiling is above player
    [HideInInspector]
    public float crouch;

    [HideInInspector]
    public bool crouching;
    [HideInInspector]
    public bool wall;
    [HideInInspector]
    public bool wallCrouch;

   

    private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	public bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;                  // Reference to the player's animator component.
    [HideInInspector]
    public Rigidbody rb;
    public GameObject gameObject;           //idk why is says it's not being used


    //air dash variables
    public bool dashEnabled;                //can this player air dash
    private bool dash;
    public float dashSpeed = 1000;          //how fast is the dash
    private bool canDash;
    private float dashTimer;
    public float dashTimerTime = 0.25f;     //how long the dash will last
    private float holdTimer;
    public float holdTimerTime = 0.1f;      //how long the end of dash will hold player


    /*
     * 
     * All this is used for ladder climbing
     */
    public bool isOnLadder = false;
    public bool isClimbing = false;
    public bool canMove = true;
    public float speed = 2f;
    private float cacheHealth;


    void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //gameObject = GetComponent<GameObject>();
        cacheHealth = PlayerHealth.visibleHealth;
	}


	void Update()
	{
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))
            || Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Enemies"))
            || Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Objects"));

        //Check if ceiling is above player
        ceiled = Physics2D.Linecast(transform.position, ceilingCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        //Check if wall infront of player
        wall = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        //wallCrouch = Physics2D.Linecast(transform.position, wallCheckCrouch.position, 1 << LayerMask.NameToLayer("Ground"));


        //Check to make sure we are on the ladder and we are pressing W before allowing climbing and disableing movement
        if(isOnLadder == true && Input.GetKey(KeyCode.W))
        {
            isClimbing = true;
            canMove = false;
        }



        if (grounded)
        {
            anim.SetBool("OnGround", true);
            canDash = true;
        }
        else
        {
            anim.SetBool("OnGround", false);
        }

        // If the jump button is pressed and the player is grounded then the player should jump.
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
                //flag for animator
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }

        // Crouch
        crouch = Input.GetAxisRaw("Crouch");
        if (crouch != 0 || ceiled == true && grounded == true)
        {
            //flag for animator
            crouching = true;
            anim.SetBool("Crouch", true);
        }
        else
        {
            crouching = false;
            anim.SetBool("Crouch", false);
        }

        if (isClimbing)
        {
            anim.SetBool("Climbing", true);
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                anim.SetBool("Climbing Move", true);
            }
            else
            {
                anim.SetBool("Climbing Move", false);
            }
        }
        else
        {
            anim.SetBool("Climbing", false);
        }


        //dashing
        if (Input.GetKeyDown(KeyCode.Mouse1) && grounded == false && canDash == true && dashEnabled == true)
        {
           dash = true;
           dashTimer = dashTimerTime;
            anim.SetBool("Dash", true);
        }

        if (dashTimer > 0)
        {
            dashTimer = dashTimer - Time.deltaTime;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            holdTimer = holdTimerTime;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            if (holdTimer > 0)
            {
               
                holdTimer -= Time.deltaTime;
                playerSpeed = 0;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                anim.SetBool("Dash", false);
            }
        }

    }


	void FixedUpdate ()
	{

        /*
         * 
         * First we check if player is climbing
         * We make sure we have disabled movement before we set isClimbing flag to be true
         * Allow controls to occurs from here instead until the player presses space or grounds themselves
         */
        if(isClimbing)
        {
            if (Input.GetKey(KeyCode.W))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            }
            else if (Input.GetKey(KeyCode.Space) || grounded || PlayerHealth.visibleHealth < cacheHealth)
            {
                isClimbing = false;
                jump = true;
                cacheHealth = PlayerHealth.visibleHealth;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.60f);
            }

        }

        // Cache the horizontal input.
        h = Input.GetAxis("Horizontal");

        //Flags character as moving for animator
        if (h != 0 && wall == false)
            anim.SetBool("IsMoving", true);
        else{
            anim.SetBool("IsMoving", false);
        }

        // making sure the charater does not fly off the screen
        playerSpeed = h * Time.deltaTime * maxSpeed;

        if (wall == true || wallCrouch == true)
        {
            playerSpeed = 0;
        }

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        if (wall == false)
        {
            anim.SetFloat("Speed", Mathf.Abs(h));
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }


        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        //if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
        //{
        //    // ... add a force to the player.
        //    //GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
        //    GetComponent<Rigidbody2D>().velocity = new Vector2(10, GetComponent<Rigidbody2D>().velocity.y);
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    GetComponent<Rigidbody2D>().velocity = new Vector2(10, GetComponent<Rigidbody2D>().velocity.y);
        //}


        //    if (Input.GetKey(KeyCode.A))
        //{
        //    GetComponent<Rigidbody2D>().velocity = new Vector2(-10, GetComponent<Rigidbody2D>().velocity.y);
        //}
        //if (Input.GetKeyUp(KeyCode.D))
        //{
        //    GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        //}

        // If the player's horizontal velocity is greater than the maxSpeed...
        //if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
        //	// ... set the player's velocity to the maxSpeed in the x axis.
        //	GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

   
        if (canMove)
        {
            transform.Translate(playerSpeed, 0, 0);
        }
        


        // If the input is moving the player right and the player is facing left...
        if (h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();

		// If the player should jump...
		if(jump)
		{
			// Set the Jump animator trigger parameter.
			anim.SetTrigger("Jump");
            canMove = true;
            isClimbing = false;

			// Play a random jump audio clip.
			int i = Random.Range(0, jumpClips.Length);
			AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

			// Add a vertical force to the player.
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}


        //air dash
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mouseDir = mousePos - gameObject.transform.position;
        mouseDir.z = 0.0f;
        mouseDir = mouseDir.normalized * dashSpeed;

        if (dash)
        {

            if(mouseDir.x < 0 && facingRight)
            {
                Flip();
            }
            else if(mouseDir.x > 0 && !facingRight)
            {
                Flip();
            }
            GetComponent<Rigidbody2D>().AddForce(new Vector2(mouseDir.x, mouseDir.y));
            dash = false;
            canDash = false;
        }
    }

    /// <summary>
    /// Check to see if we are intersecting with ladder
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isOnLadder = true;
        }
    }

    /// <summary>
    /// When we exit we reset isOnLadder
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        isOnLadder = false;
    }


    void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	public IEnumerator Taunt()
	{
		// Check the random chance of taunting.
		float tauntChance = Random.Range(0f, 100f);
		if(tauntChance > tauntProbability)
		{
			// Wait for tauntDelay number of seconds.
			yield return new WaitForSeconds(tauntDelay);

			// If there is no clip currently playing.
			if(!GetComponent<AudioSource>().isPlaying)
			{
				// Choose a random, but different taunt.
				tauntIndex = TauntRandom();

				// Play the new taunt.
				GetComponent<AudioSource>().clip = taunts[tauntIndex];
				GetComponent<AudioSource>().Play();
			}
		}
	}


	int TauntRandom()
	{
		// Choose a random index of the taunts array.
		int i = Random.Range(0, taunts.Length);

		// If it's the same as the previous taunt...
		if(i == tauntIndex)
			// ... try another random taunt.
			return TauntRandom();
		else
			// Otherwise return this index.
			return i;
	}
}
