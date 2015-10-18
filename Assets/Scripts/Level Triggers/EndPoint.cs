using UnityEngine;
using System.Collections;

public class EndPoint : MonoBehaviour {

	public PauseMenu pauseMenu;
	public EndMenu endMenu;
	public GameObject endMenuCanvas;
	public AudioClip[] audioClip;
    public TimeControl timeControl;
    public PlayerInput playerInput;
    AudioSource audio;
	public bool isPaused;
    string level;
    float best;

    public string replayString = "";

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

            //Create the replay string to send to database.
            MakeReplayString();

            if(PlayerPrefs.GetInt("isReplay") == 1)
            {
                PlayerPrefs.SetInt("isReplay", 0);
            }
            
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
        total += coins;

        //Write the new value back to the player prefs
        PlayerPrefs.SetInt("coinCount", total);
    }

    void PlaySound (int clip) {
		audio.clip = audioClip [clip];
		audio.Play ();
	}


    //Create the replayString to send to the database.
    void MakeReplayString()
    {
        
        for(int i = 0; i < PlayerInput.currentPosition.Count; i++)
        {
            replayString = replayString + PlayerInput.currentPosition[i].x + "," + PlayerInput.currentPosition[i].y + "," + PlayerInput.currentPosition[i].z + "," + PlayerInput.currentPosition[i].w + ";";
            if(i == PlayerInput.currentPosition.Count - 1)
            {
                replayString = replayString + PlayerInput.currentPosition[i].x + "," + (PlayerInput.currentPosition[i].y) + "," + PlayerInput.currentPosition[i].z + "," + PlayerInput.currentPosition[i].w + ";";
            }

        }

        

        replayString = replayString + System.Environment.NewLine;

        //for(int i = 0; i < PlayerInput.jumpInputs.Count; i++)
        //{
          //  replayString = replayString + PlayerInput.jumpInputs[i].x + "," + PlayerInput.jumpInputs[i].y + ";";
        //}

        replayString = Application.loadedLevelName + System.Environment.NewLine + replayString;


        PlayerPrefs.SetString("replayString", replayString);

        Debug.Log(replayString);
        Debug.Log("Finished making replay string.");
    }
}
