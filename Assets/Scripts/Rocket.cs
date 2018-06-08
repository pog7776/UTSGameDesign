using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    public float rocketLife = 2;        //how long the rocket will survive before disappearing if not exploded
    public GameObject explosion;		// Prefab of explosion effect.
    public bool ExplosionTriggerProjectile = false;
    public float hedukenLenght = 1;
    public float hedukenAmount = 1;
    public bool heduken = false;                //trigger to make projectiles weird

    //handle camera shaking
    public float camShakeAmt = 0.1f;
    public float camShakeLength = 0.2f;
    CameraShake camShake;
    public bool doCameraShake;
    private Scene currentScene;


    void Start()
    {
        // Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
        Destroy(gameObject, rocketLife);

        camShake = GameMaster.gm.GetComponent<CameraShake>();
        if(camShake == null)
        {
            Debug.LogError("No CameraShake found...");
        }


        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Level4" || currentScene.name =="ControlRoom" || currentScene.name == "End")
        {
            doCameraShake = false;
        }
        else
        {
            doCameraShake = true;
        }
    }


    void OnExplode()
    {
        // Create a quaternion with a random rotation in the z-axis.
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

        // Instantiate the explosion where the rocket is with the random rotation.
        Instantiate(explosion, transform.position, randomRotation);

        //camera shake
        if (doCameraShake == true)
        {
            camShake.Shake(camShakeAmt, camShakeLength);
        }

        if (heduken == true)
        {
            //IDK why this does this... dont turn it on unless you want crazy projectiles
            Heduken.Shake(hedukenLenght, hedukenAmount);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // If it hits an enemy...
        if (col.tag == "Enemy")
        {
            // ... find the Enemy script and call the Hurt function.
            col.gameObject.GetComponent<Enemy>().Hurt();

            // Call the explosion instantiation.
            OnExplode();

            // Destroy the rocket.
            Destroy(gameObject);
        }
        else if (col.tag == "Enemy_Fly")
        {
            // ... find the Enemy script and call the Hurt function.
            col.gameObject.GetComponent<Enemy_Fly>().Hurt();

            // Call the explosion instantiation.
            OnExplode();

            // Destroy the rocket.
            Destroy(gameObject);
        }

        else if (col.tag == "Blade_Enemy")
        {
            // ... find the Enemy script and call the Hurt function.
            col.gameObject.GetComponent<BladeEnemy>().Hurt();

            // Call the explosion instantiation.
            OnExplode();

            // Destroy the rocket.
            Destroy(gameObject);
        }

        // Otherwise if it hits a bomb crate...     need to remove this stuff
        else if (col.tag == "BombPickup")
        {
            // ... find the Bomb script and call the Explode function.
            col.gameObject.GetComponent<Bomb>().Explode();

            // Destroy the bomb crate.
            Destroy(col.transform.root.gameObject);

            // Destroy the rocket.
            Destroy(gameObject);
        }

        // Objects to exclude
        else if (col.gameObject.tag == "Player" || col.gameObject.tag == "Door" || col.gameObject.tag == "Collectable" || col.gameObject.tag == "Health" || col.gameObject.tag == "Ladder" || col.gameObject.tag == "CheckPoint")
        {
            //Put stuff here
        }

        //Rocket collide with explosion
        else if (col.gameObject.tag != "Explosion" && ExplosionTriggerProjectile == true)
        {
            OnExplode();
            Destroy(gameObject);
        }

        //Things to collide with
        else if (col.gameObject.tag != "Wall" || col.gameObject.tag != "Ground" || col.gameObject.tag != "Objects" || col.gameObject.tag != "HealthBar" || col.gameObject.tag != "KillPlain" )
        {
            OnExplode();
            Destroy(gameObject);
        }
    }
}