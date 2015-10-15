using UnityEngine;
using System.Collections;

public class EndPoint : MonoBehaviour {

	public PauseMenu pauseMenu;
	public EndMenu endMenu;
	public GameObject endMenuCanvas;
	public AudioClip[] audioClip;
	AudioSource audio;
	public bool isPaused;
    string level;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isPaused) {
			Time.timeScale = 0f;
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			PlaySound (0);
			pauseMenu.disabled = true;
			isPaused = true;
			endMenu.display ();
			endMenuCanvas.SetActive (true);

            // Set local storage to unlock next level (remove padlock from Level Select GUI)
            level = Application.loadedLevelName;
            switch (level) {
                case "level1":
                    PlayerPrefs.SetString("level2Locked", "false");
                    break;
                case "level2":
                    PlayerPrefs.SetString("level3Locked", "false");
                    break;
                case "level3":
                    PlayerPrefs.SetString("level4Locked", "false");
                    break;
                default:
                    break;
            }
        }
	}

	void PlaySound (int clip) {
		audio.clip = audioClip [clip];
		audio.Play ();
	}
}
