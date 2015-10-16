﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour {

    public GameObject canvas;
    float best;

    // Represents which levels are locked
    string level2Locked;
    string level3Locked;
    string level4Locked;

    // Custom colors
    Color yellow = new Color(255f, 248f, 0f);
    Color grey = new Color(255f, 255f, 255f);

    // Level buttons
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;

    // Level 1 stars
    public GameObject star11;
    public GameObject star12;
    public GameObject star13;

    // Level 2 stars
    public GameObject star21;
    public GameObject star22;
    public GameObject star23;

    // Level 3 stars
    public GameObject star31;
    public GameObject star32;
    public GameObject star33;

    // Level 4 stars
    public GameObject star41;
    public GameObject star42;
    public GameObject star43;

    // Level button lock images
    public GameObject lock2;
    public GameObject lock3;
    public GameObject lock4;

    // Set time required to unlock Bronze/Silver/Gold star rating for each level
    float[,] benchmark = new float[3, 4] { { 90, 120, 180, 60 }, { 20, 50, 90, 45 }, { 15, 35, 45, 30 } };

    void Start() {
        /* //Debug.Log("Level2Locked is currently :" + PlayerPrefs.GetString("Level2Locked"));
         //Debug.Log("Level3Locked is currently :" + PlayerPrefs.GetString("Level3Locked"));
         //Debug.Log("Level4Locked is currently :" + PlayerPrefs.GetString("Level4Locked"));

         if (PlayerPrefs.GetString("Level2Locked").Equals("false"))
         {
             Debug.Log("level2button should be interactable");
             level2Button.interactable = true;
             //level2Button.image.color = new Color32(29, 211, 20, 255); //green colour (level enabled).

         }
         else
         {
             //Debug.Log("level2button should NOT be interactable");
             level2Button.interactable = false;

         }

         if (PlayerPrefs.GetString("Level3Locked").Equals("false"))
         {
             Debug.Log("level3button interactability is : " + level3Button.interactable);
             level3Button.interactable = true;
             //level3Button.image.color = new Color32(29, 211, 20, 255); //green colour (level enabled).
         }
         else
         {
             //Debug.Log("level3button interactability is : " + level3Button.interactable);
             level3Button.interactable = false;
         }

         if (PlayerPrefs.GetString("Level4Locked").Equals("false"))
         {
             level4Button.interactable = true;
             //level4Button.image.color = new Color32(29, 211, 20, 255); //green colour (level enabled).
         }
         else
         {
             level4Button.interactable = false;
         }
         */

        // Set stars for level 1 based on player's best time
        setStars(1, star11, star12, star13);

        level2Locked = PlayerPrefs.GetString("level2Locked");
        // If level 2 is unlocked, allow user to click level 2, hide the lock and show the stars
        if (level2Locked.Equals("false")) {
            level2Button.interactable = true;
            lock2.SetActive(false);
            setStars(2, star21, star22, star23);
        }

        level3Locked = PlayerPrefs.GetString("level3Locked");
        // If level 3 is unlocked, allow user to click level 3
        if (level3Locked.Equals("false")) {
            level3Button.interactable = true;
            lock3.SetActive(false);
            setStars(3, star31, star32, star33);
        }

        level4Locked = PlayerPrefs.GetString("level4Locked");
        // If level 4 is unlocked, allow user to click level 4
        if (level4Locked.Equals("false")) {
            level4Button.interactable = true;
            lock4.SetActive(false);
            setStars(4, star41, star42, star43);
        }
    }

        // Set stars to gold to mirror player's best time for specified level
        void setStars(int level, GameObject star1, GameObject star2, GameObject star3) {
            // Display stars on level icons
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
            // Get player's best time for specified level
            best = PlayerPrefs.GetFloat("level" + level.ToString() + "Best");
        // Check that best time is not 0 (i.e. not set yet), and color the stars according to their performance
        if (best != 0) {
                if (best <= benchmark[0, level - 1]) {
                    star1.GetComponent<Image>().color = yellow;
                }
                if (best <= benchmark[1, level - 1]) {
                    star2.GetComponent<Image>().color = yellow;
                }
                if (best <= benchmark[2, level - 1]) {
                    star3.GetComponent<Image>().color = yellow;
                }
            }
        }
}
