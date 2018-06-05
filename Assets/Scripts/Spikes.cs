using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Spikes : MonoBehaviour
{
	public GameObject splash;
    public PlayerControl player;
    public PlayerHealth playerHealth;

    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }


    void OnTriggerEnter2D(Collider2D col)
	{
		// If the player hits the trigger...
		if(col.gameObject.tag == "Player")
		{
			// .. stop the camera tracking the player
			//GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;

			// ... instantiate the splash where the player falls in.
			Instantiate(splash, col.transform.position, transform.rotation);
            // ... destroy the player.
            //Destroy (col.gameObject);
            // GameObject.FindGameObjectWithTag("Player").SetActive(false);
            // // ... reload the level.
            // StartCoroutine("ReloadGame");
            Respawn();
		}
		else
		{

            if (col.gameObject.tag == "Bullet" || col.gameObject.tag == "Objects")
            {

            }

            if(col.gameObject.tag == "Enemy")
            {
                // ... instantiate the splash where the enemy falls in.
                Instantiate(splash, col.transform.position, transform.rotation);

                // Destroy the enemy.
                Destroy(col.gameObject);
                //Respawn();
            }
		}
	}

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(2);
		// ... and then reload the level.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}


    private void Respawn()
    {
        CheckPointController checkPoint = FindObjectOfType<CheckPointController>();

        CheckPoints check = checkPoint.GetLastCheckPoint();
        playerHealth.SetHealth();
        player.SetPosition(check.checkPointPos);

    }
}
