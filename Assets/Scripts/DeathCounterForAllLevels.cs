using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DeathCounterForAllLevels : MonoBehaviour {

    public Text deathCounter;
    private static int deathAmount = 0;
    private static int deathForLevelOne = 0;
    private static int deathForLevelTwo = 0;
    private static int deathForLevelThree = 0;
    private static int deathForLevelFour = 0;
    private static int deathForLevelFive = 0;

    private string sceneName;



    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        deathCounter = GameObject.Find("Death").GetComponent<Text>();
        if(sceneName == "Menu")
        {
            deathCounter.text = "Total Deaths: " + deathAmount;
        }
        else
        deathCounter.text = "Total Deaths: " + deathAmount + "\n" + "Level Death: " + 0;

    }

    // Update is called once per frame
    void Update () {
        
		
	}

    public void AddDeath()
    {
        deathAmount++;

        switch (sceneName)
        {
            case "Level":
                deathForLevelOne++;
                deathCounter.text = "Total Deaths: " + deathAmount + "\n" + "Level Death: " + deathForLevelOne;
                break;                                                             
            case "Level99":                                                        
                deathForLevelTwo++;                                                
                deathCounter.text = "Total Deaths: " + deathAmount + "\n" + "Level Death: " + deathForLevelTwo;
                break;                                                             
            case "Level3":                                                         
                deathForLevelThree++;                                              
                deathCounter.text = "Total Deaths: " + deathAmount + "\n" + "Level Death: " + deathForLevelThree;
                break;                                                             
            case "Level4":                                                         
                deathForLevelFour++;                                               
                deathCounter.text = "Total Deaths: " + deathAmount + "\n" + "Level Death: " + deathForLevelFour;
                break;                                                             
            case "Level5":                                                         
                deathForLevelFive++;                                               
                deathCounter.text = "Total Deaths: " + deathAmount + "\n" + "Level Death: " + deathForLevelFive;
                break;
            case "Menu":
                deathCounter.text = "Total Deaths: " + deathAmount;
                break;
            case "ControlRoom":
                deathCounter.text = "Total Deaths: " + deathAmount;
                break;
            case "End":
                deathCounter.text = "Total Deaths: " + deathAmount;
                break;
            default:
                break;
        }
    }
}
