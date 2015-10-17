using UnityEngine;
using System.Collections;

public class AchievementManager : MonoBehaviour {

    // Total number of coins the player has collected
    int coinCount;

    // Represents whether a level is locked or not ("true" or "false") - stored in local storage
    string level2Locked;
    string level3Locked;
    string level4Locked;

    // Green complete ticks for achievements 1-4
    public GameObject Complete1;
    public GameObject Complete2;
    public GameObject Complete3;
    public GameObject Complete4;

    // Padlock for achievements 1-4
    public GameObject Lock1;
    public GameObject Lock2;
    public GameObject Lock3;
    public GameObject Lock4;

    // Add ticks to achievements which have been completed
    void Start() {

        UpdateAchievements();

    }

    // Checks which achievements are complete
    void UpdateAchievements() {
        UpdateAchievement1();
        UpdateAchievement2();
        UpdateAchievement3();
        UpdateAchievement4();
    }

    void UpdateAchievement1() {
        // Get level locked status from local storage
        level2Locked = PlayerPrefs.GetString("level2Locked");
        level3Locked = PlayerPrefs.GetString("level3Locked");
        level4Locked = PlayerPrefs.GetString("level4Locked");
        // Check if that all levels are unlocked
        if ((level2Locked.Equals("false")) && (level3Locked.Equals("false")) && (level4Locked.Equals("false"))) {
            // Hide lock
            Lock1.SetActive(false);
            // Display completion tick
            Complete1.SetActive(true);
        }
    }

    void UpdateAchievement2() {
        // Get total number of coins collected by player
        coinCount = PlayerPrefs.GetInt("coinCount");
        // Check if player has collected atleast 1,000 coins
        if (coinCount >= 1000) {
            // Hide lock
            Lock2.SetActive(false);
            // Display completion tick
            Complete2.SetActive(true);
        }
    }

    void UpdateAchievement3() {

    }

    void UpdateAchievement4() {

    }

}
