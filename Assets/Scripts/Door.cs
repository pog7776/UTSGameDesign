using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public GameObject DoorSprite;
    public GameObject startPosition;    //where the door sprite starts
    public GameObject endPosition;      //where the door sprite will end up
    public float moveSpeed = 10.0f;     //speed of the door closing
    public float timer = 10;            //time until door closes
    public bool count = false;          //begin counting down timer
    

    // Use this for initialization
    void Start()
    {
        DoorSprite.transform.position = new Vector3(startPosition.transform.position.x, startPosition.transform.position.y, startPosition.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //start timer if count == ture
        if (count == true)
        {
            timer = timer -= Time.deltaTime;
        }

        //when timer ends close the door
        if (timer <= 0)
        {
            float step = moveSpeed * Time.deltaTime;
            DoorSprite.transform.position = Vector3.Lerp(startPosition.transform.position, endPosition.transform.position, step);
        }
    }

    //used to trigger the door closing as the player touches it
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            count = true;
        }
    }
}