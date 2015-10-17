using UnityEngine;
using System.Collections;

public class EndPoint : MonoBehaviour {

	public PauseMenu pauseMenu;
	public EndMenu endMenu;
	public GameObject endMenuCanvas;
	public AudioClip[] audioClip;
    public TimeControl timeControl;
    AudioSource audio;
	public bool isPaused;
    string level;
    float best;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		isPaused = false;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {

        // Update level select GUI to show player progress through each of the 4 different levels
        UpdateProgress();

            PlaySound (0);
			pauseMenu.disabled = true;
            UpdateCoinCount(endMenu.coinCount.coinCount);

            endMenu.timeControl.StopTimer();
			endMenu.display ();
			endMenuCanvas.SetActive (true);

            other.gameObject.SetActive(false);
		}
	}

    void UpdateProgress() {
        // Get time taken for current level
        float time = timeControl.time;
        // Get current level
        level = Application.loadedLevelName;
        // Set local storage to unlock next level (remove padlock from Level Select GUI) and update best time
        switch (level) {
            case "level1":
                PlayerPrefs.SetString("level2Locked", "false");
                UpdateBest(1, time);
                break;
            case "level2":
                PlayerPrefs.SetString("level3Locked", "false");
                UpdateBest(2, time);
                break;
            case "level3":
                PlayerPrefs.SetString("level4Locked", "false");
                UpdateBest(3, time);
                break;
            case "level4":
                UpdateBest(4, time);
                break;
            default:
                break;
        }
        PlayerPrefs.Save();
    }

    void UpdateBest(int level, float time) {
        best = PlayerPrefs.GetFloat("level" + level.ToString() + "Best");
        if (time < best || best == 0) {
            PlayerPrefs.SetFloat("level" + level.ToString() + "Best", time);
        }
        PlayerPrefs.Save();
    }

    //Adds the coin amount to the players total coins
    void UpdateCoinCount(int coins) {
        int total = PlayerPrefs.GetInt("coinCount");
        Debug.Log(total);
        total += coins;
        Debug.Log(total);

        //Write the new value back to the player prefs
        PlayerPrefs.SetInt("coinCount", total);
    }

    void PlaySound (int clip) {
		audio.clip = audioClip [clip];
		audio.Play ();
	}
}
