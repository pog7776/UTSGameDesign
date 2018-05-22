using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour
{
    public float rocketLife = 2;        //how long the rocket will survive before disappearing if not exploded
    public GameObject explosion;		// Prefab of explosion effect.
    public bool ExplosionTriggerProjectile = false;


    void Start()
    {
        // Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
        Destroy(gameObject, rocketLife);
    }


    void OnExplode()
    {
        // Create a quaternion with a random rotation in the z-axis.
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

        // Instantiate the explosion where the rocket is with the random rotation.
        Instantiate(explosion, transform.position, randomRotation);
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
        // Otherwise if it hits a bomb crate...
        else if (col.tag == "BombPickup")
        {
            // ... find the Bomb script and call the Explode function.
            col.gameObject.GetComponent<Bomb>().Explode();

            // Destroy the bomb crate.
            Destroy(col.transform.root.gameObject);

            // Destroy the rocket.
            Destroy(gameObject);
        }
        // Otherwise if the player manages to shoot himself...
        else if (col.gameObject.tag != "Player" || col.gameObject.tag != "Explosion")
        {
            // Instantiate the explosion and destroy the rocket.
            OnExplode();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Explosion" && ExplosionTriggerProjectile == false)
        {
            Physics.IgnoreCollision(explosion.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}