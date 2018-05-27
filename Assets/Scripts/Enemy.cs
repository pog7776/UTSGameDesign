using UnityEngine;
using System.Collections;
using System;
using System.Timers;

public class Enemy : MonoBehaviour
{

    public enum EnemyBehaviour
    {
        Patrol = 0,
        Attack = 1
    }

    private Animator anim;			    // Reference to the animator component.
    public float moveSpeed = 2f;        // The speed the enemy moves at.
    public int HP = 2;                  // How many times the enemy can be hit before it dies.
    public Sprite deadEnemy;            // A sprite of the enemy when it's dead.
    public Sprite damagedEnemy;         // An optional sprite of the enemy when it's damaged.
    public AudioClip[] deathClips;      // An array of audioclips that can play when the enemy dies.
    public GameObject hundredPointsUI;  // A prefab of 100 that appears when the enemy dies.
    public float deathSpinMin = -100f;          // A value to give the minimum amount of Torque when dying
    public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying
    public bool isHittingWall = false;
    public bool isHittingPlayer = false;
    public EnemyBehaviour enemyBehaviour = EnemyBehaviour.Patrol;
    public bool isGrounded;
    public bool searching = false;
    private SpriteRenderer ren;         // Reference to the sprite renderer.
    private Transform frontCheck;       // Reference to the position of the gameobject used for checking if something is in front.
    private bool dead = false;          // Whether or not the enemy is dead.
                                        //private Score score;				// Reference to the Score script.

    public LayerMask enemyMask;
    public LayerMask playerMask;
    Rigidbody2D myRigidbody;
    Transform myTransform;
    float width;
    public Transform playerTransform;
    System.Timers.Timer aTimer = new System.Timers.Timer();

    private void Start()
    {
        myTransform = this.transform;
        myRigidbody = this.GetComponent<Rigidbody2D>();
        width = this.GetComponent<SpriteRenderer>().bounds.extents.x;
    }




    void Awake()
    {
        // Setting up the references.
        //ren = transform.Find("body").GetComponent<SpriteRenderer>();
        //frontCheck = transform.Find("frontCheck").transform;
        //score = GameObject.Find("Score").GetComponent<Score>();
    }

    void FixedUpdate()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
            // GameObject.FindWithTag("enemy").SetActive = false;
            //anim.SetTrigger("Dead");
        }


        if(DetectPlayer(new Vector2(myTransform.localPosition.x, myTransform.localPosition.y), 3))
        {
            enemyBehaviour = EnemyBehaviour.Attack;
        }
        else
        {
            enemyBehaviour = EnemyBehaviour.Patrol;
        }

        Vector2 linecast = myTransform.position + myTransform.right * width;
        if ((int)myTransform.eulerAngles.y == 0)
        {
            // Debug.DrawLine(linecast, linecast + Vector2.right);
            isHittingWall = Physics2D.Linecast(linecast, linecast + Vector2.right, enemyMask);
            //isHittingPlayer = Physics2D.Linecast(linecast, linecast + Vector2.right * 2, playerMask);
        }
        else
        {
            // Debug.DrawLine(linecast, linecast + Vector2.left);
            isHittingWall = Physics2D.Linecast(linecast, linecast + Vector2.left, enemyMask);
            //isHittingPlayer = Physics2D.Linecast(linecast, linecast + Vector2.left * 2, playerMask);

        }

        //if (isHittingPlayer)
        //{
        //    enemyBehaviour = EnemyBehaviour.Attack;
        //}
        //
        //else
        //{
        //    enemyBehaviour = EnemyBehaviour.Patrol;
        //   // StartCoroutine(ChangePatrol());
        //}

        //Todo - move this terrible code into fucntions
        //and use a switch statement for the behaviour
        //fix how it follows the player and how quickly it changes back into patrol
        if (enemyBehaviour == EnemyBehaviour.Patrol)
        {

            //Debug.DrawLine(linecast, linecast + Vector2.down);
            isGrounded = Physics2D.Linecast(linecast, linecast + Vector2.down, enemyMask);

            if (!isGrounded || isHittingWall)
            {
                
                Flip();
            }

            Vector2 myVelocity = myRigidbody.velocity;
            myVelocity.x = myTransform.right.x * moveSpeed ;
            myRigidbody.velocity = myVelocity;
        }

        if (enemyBehaviour == EnemyBehaviour.Attack)
        {
            bool isGrounded = Physics2D.Linecast(linecast, linecast + Vector2.down, enemyMask);
            if (!isGrounded)
            {
                moveSpeed = 0f;
                //Vector3 currentRotation = myTransform.eulerAngles;
                //currentRotation.y += 180;
                //myTransform.eulerAngles = currentRotation;
            }
            else
            {
                moveSpeed = 2f;
            }

            //Vector2 myVelocity = myRigidbody.velocity;
            //myVelocity.x = myTransform.right.x * moveSpeed;
            //myRigidbody.velocity = myVelocity;
            myTransform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }

    }


    public void Hurt()
    {
        // Reduce the number of hit points by one.
        HP--;
    }

    void Death()
    {
        // Find all of the sprite renderers on this object and it's children.
        SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

        // Disable all of them sprite renderers.
        foreach (SpriteRenderer s in otherRenderers)
        {
            s.enabled = false;
        }

        // Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
        ren.enabled = true;
        ren.sprite = deadEnemy;

        // Increase the score by 100 points
        //score.score += 100;

        // Set dead to true.
        dead = true;

        // Allow the enemy to rotate and spin it by adding a torque.
        GetComponent<Rigidbody2D>().AddTorque(UnityEngine.Random.Range(deathSpinMin, deathSpinMax));

        // Find all of the colliders on the gameobject and set them all to be triggers.
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
        {
            c.isTrigger = true;
        }

        // Play a random audioclip from the deathClips array.
        int i = UnityEngine.Random.Range(0, deathClips.Length);
        AudioSource.PlayClipAtPoint(deathClips[i], transform.position);

        // Create a vector that is just above the enemy.
        Vector3 scorePos;
        scorePos = transform.position;
        scorePos.y += 1.5f;

        // Instantiate the 100 points prefab at this point.
        Instantiate(hundredPointsUI, scorePos, Quaternion.identity);
    }

    private bool DetectPlayer(Vector2 centre, float radius)
    {
        Collider2D collider = Physics2D.OverlapCircle(centre, radius, playerMask);
        if(collider != null)
        {
            if (collider.CompareTag("Player"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }

    }

    public void Flip()
    {
        // Multiply the x component of localScale by -1.
        Vector3 currentRotation = myTransform.eulerAngles;
        currentRotation.y += 180;
        myTransform.eulerAngles = currentRotation;
    }
}
