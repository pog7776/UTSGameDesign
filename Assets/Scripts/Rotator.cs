using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public float rotationDegreesPerSecond = 45f;
    public float rotationDegreesAmount = 360f;
    private float totalRotation = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if we haven't reached the desired rotation, swing

        if (Mathf.Abs(totalRotation) < Mathf.Abs(rotationDegreesAmount))
            SwingOpen();
            totalRotation = 0;
    }

    void SwingOpen()
    {
        float currentAngle = transform.rotation.eulerAngles.y;
        transform.rotation =
         Quaternion.AngleAxis(currentAngle + (Time.deltaTime * rotationDegreesPerSecond), Vector3.up);
        totalRotation += Time.deltaTime * rotationDegreesPerSecond;
    }
}
