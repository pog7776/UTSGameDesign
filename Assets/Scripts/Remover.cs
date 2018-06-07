using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Remover : MonoBehaviour
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

			// .. stop the Health Bar following the player
			//if(GameObject.FindGameObjectWithTag("HealthBar").activeSelf)
			//{
			//	GameObject.FindGameObjectWithTag("HealthBar").SetActive(false);
			//}
            //
			//// ... instantiate the splash where the player falls in.
			//Instantiate(splash, col.transform.position, transform.rotation);
            // ... destroy the player.
            //Destroy (col.gameObject);
            //GameObject.FindGameObjectWithTag("Player").SetActive(false);
            // ... reload the level.
            playerHealth.health = 0;
           // StartCoroutine("Respawn");

           // StartCoroutine("ReloadGame");
		}
		else
		{
			// ... instantiate the splash where the enemy falls in.
			Instantiate(splash, col.transform.position, transform.rotation);

			// Destroy the enemy.
			Destroy (col.gameObject);	
		}
	}

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(2);
		// ... and then reload the level.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

    IEnumerator Respawn()
    {
        CheckPointController checkPoint = FindObjectOfType<CheckPointController>();

        CheckPoints check = checkPoint.GetLastCheckPoint();

        yield return new WaitForSeconds(0.5f);


        playerHealth.health = check.health;
        player.SetPosition(check.checkPointPos);
        
    }
}
