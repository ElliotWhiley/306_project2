using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour {

    string level2Locked;
    string level3Locked;
    string level4Locked;
    public GameObject canvas;

    // Custom colors
    Color yellow = new Color(255f, 248f, 0f);
    Color grey = new Color(255f, 255f, 255f);

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
    public GameObject star43 ;

    // Level button lock images
    public GameObject lock2;
    public GameObject lock3;
    public GameObject lock4;


    // Use this for initialization
    void Start () {
        level2Locked = PlayerPrefs.GetString("level2Locked");
        // If level 2 is unlocked, allow user to click level 2, hide the lock and show the stars
        if (level2Locked.Equals("false")) {
            level2Button.interactable = true;
            lock2.SetActive(false);
            star21.SetActive(true);
            star22.SetActive(true);
            star23.SetActive(true);
        }

        level3Locked = PlayerPrefs.GetString("level3Locked");
        // If level 3 is unlocked, allow user to click level 3
        if (level3Locked.Equals("false")) {
            level3Button.interactable = true;
            lock3.SetActive(false);
            star31.SetActive(true);
            star32.SetActive(true);
            star33.SetActive(true);
        }

        level4Locked = PlayerPrefs.GetString("level4Locked");
        // If level 4 is unlocked, allow user to click level 4
        if (level4Locked.Equals("false")) {
            level4Button.interactable = true;
            lock4.SetActive(false);
            star41.SetActive(true);
            star42.SetActive(true);
            star43.SetActive(true);
        }
    }
	
    // 
    void setStars(string level, GameObject star1, GameObject star2, GameObject star3) {
        string best;
    }

}
