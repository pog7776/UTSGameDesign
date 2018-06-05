using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour {


    public CheckPoints[] checkpoints;
    public CheckPoints lastCheckPoint;
    public int checkPointAmount;

	// Use this for initialization
	void Start () {
        checkpoints = new CheckPoints[checkPointAmount];
        FindAllCheckPoints();
	}
	
	// Update is called once per frame
	void Update () {
        GetLastCheckPoint();
	}


    void FindAllCheckPoints()
    {
        checkpoints = FindObjectsOfType<CheckPoints>();
    }

    public CheckPoints GetLastCheckPoint()
    {
        for(int i = 0; i < checkpoints.Length - 1; i++)
        {
            if(checkpoints[i].isTriggered)
            {
                lastCheckPoint = checkpoints[i];
            }
        }
        return lastCheckPoint;
    }

    public void SetAllCheckPointsToFalse()
    {
        for (int i = 0; i < checkpoints.Length - 1; i++)
        {
            checkpoints[i].isTriggered = false;
        }
    }
}
