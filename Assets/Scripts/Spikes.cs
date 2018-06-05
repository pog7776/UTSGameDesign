using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Spikes : MonoBehaviour
{
	public GameObject splash;


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
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
            // ... reload the level.
            StartCoroutine("ReloadGame");
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
}
