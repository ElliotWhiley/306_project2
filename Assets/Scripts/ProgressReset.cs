using UnityEngine;
using System.Collections;


/**
Use this script whenever one wants to reset the player's progress in the local save files. 

    */
public class ProgressReset : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        PlayerPrefs.SetString("Level2Locked", "true");
        PlayerPrefs.SetString("Level3Locked", "true");
        PlayerPrefs.SetString("Level4Locked", "true");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
