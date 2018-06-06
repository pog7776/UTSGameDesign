using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{	
	public float health = 100f;					// The player's health.
	public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
	public AudioClip[] ouchClips;				// Array of clips to play when the player is damaged.
	public float hurtForce = 10f;				// The force with which the player is pushed when hurt.
	public float damageAmount = 10f;			// The amount of damage to take when enemies touch the player
    public float rocketDamageAmount = 5f;       // Damage rocket causes to player
    public GameObject explosion;
    public bool rocketDamagePlayer = true;
    public static float visibleHealth = 100;

    public HealthPickup heal;

	private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
	private float lastHitTime;					// The time at which the player was last hit.
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
	private PlayerControl playerControl;		// Reference to the PlayerControl script.
	private Animator anim;						// Reference to the Animator on the player
    public float healAmount;

    public float lives = 3; //amount of lives before truly dead

    private void Start()
    {
        GameObject healer = GameObject.FindGameObjectWithTag("Health");

        heal = healer.GetComponent<HealthPickup>();
    }

    public void SetHealth()
    {
        health = 100f;
    }

    void Awake ()
	{
		// Setting up references.
		playerControl = GetComponent<PlayerControl>();
		healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();

		// Getting the intial scale of the healthbar (whilst the player has full health).
		healthScale = healthBar.transform.localScale;

        visibleHealth = health;
	}

    private void Update()
    {
        visibleHealth = health;

        if(heal.heal == true)
        {
            health = HealthPickup.newHealth;
            heal.heal = false;
        }

        if (health > 100)
        {
            health = 100;
        }

        UpdateHealthBar();
    }


    void OnCollisionEnter2D (Collision2D col)
	{
		// If the colliding gameobject is an Enemy...
		if(col.gameObject.tag == "Enemy")
		{
			// ... and if the time exceeds the time of the last hit plus the time between hits...
			if (Time.time > lastHitTime + repeatDamagePeriod) 
			{
				// ... and if the player still has health...
				if(health > 0f)
				{
					// ... take damage and reset the lastHitTime.
					TakeDamage(col.transform); 
					lastHitTime = Time.time; 
				}
				// If the player doesn't have health, die
				else
				{
                    Die();
				}
			}
		}

        //damage from rocket
        if (col.gameObject.tag == "Explosion" && rocketDamagePlayer == true)
        {
             // ... and if the player still has health...
            if (health > 0f)
            {
                  RocketDamage();
            }
             // If the player doesn't have health, die
            else
            {
                Die();
            }
        }
    }


    void TakeDamage (Transform enemy)
	{
		// Make sure the player can't jump.
		playerControl.jump = false;

		// Create a vector that's from the enemy to the player with an upwards boost.
		Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;

		// Add a force to the player in the direction of the vector and multiply by the hurtForce.
		GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);

		// Reduce the player's health by 10.
		health -= damageAmount;

		// Update what the health bar looks like.
		UpdateHealthBar();

		// Play a random clip of the player getting hurt.
		int i = Random.Range (0, ouchClips.Length);
		AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
	}

    public void RocketDamage()
    {
        // Make sure the player can't jump.
        playerControl.jump = false;

        // Reduce the player's health by rocketDamageAmount.
        health -= rocketDamageAmount;

        // Update what the health bar looks like.
        UpdateHealthBar();

        // Play a random clip of the player getting hurt.
        int i = Random.Range(0, ouchClips.Length);
        AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
    }


    public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);

		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
	}

    public void Die()
    {
        // Find all of the colliders on the gameobject and set them all to be triggers.
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
        {
            //c.isTrigger = true;
        }

        if (lives == 0)
        {
            // Move all sprite parts of the player to the front
            SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer s in spr)
            {
                s.sortingLayerName = "UI";
            }

            // ... disable user Player Control script
            GetComponent<PlayerControl>().enabled = false;

            // ... disable the Gun script to stop a dead guy shooting a nonexistant bazooka
            GetComponentInChildren<Gun>().enabled = false;

            // ... Trigger the 'Die' animation state
            anim.SetTrigger("Die");
            anim.SetTrigger("Dead");
        }
        else
        {
            lives--;
            StartCoroutine("Respawn");
            anim.SetTrigger("Die");
            anim.SetTrigger("Dead");
            
        }

    }

    IEnumerator Respawn()
    {


        CheckPointController checkPoint = FindObjectOfType<CheckPointController>();
        CheckPoints check = checkPoint.GetLastCheckPoint();
        yield return new WaitForSeconds(0.5f);
        this.health = check.health;
        playerControl.SetPosition(check.checkPointPos);
        anim.SetBool("OnGround", true);

    }
}
