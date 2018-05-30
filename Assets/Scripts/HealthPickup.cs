using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    public float healAmount;
    public static float newHealth = 0;
    public bool heal;

    // Use this for initialization
    void Start()
    {
        newHealth = PlayerHealth.visibleHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            newHealth = PlayerHealth.visibleHealth + healAmount;
            gameObject.SetActive(false);
            heal = true;
        }
    }
}
