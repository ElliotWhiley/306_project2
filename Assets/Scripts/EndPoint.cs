using UnityEngine;
using System.Collections;

public class EndPoint : MonoBehaviour {

	public PauseMenu pauseMenu;
	public EndMenu endMenu;
	public GameObject endMenuCanvas;
	public AudioClip[] audioClip;
	AudioSource audio;
	public bool isPaused;

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


            //16 seconds cutoff to unlock level 2
            if (Application.loadedLevelName.Equals("level1"))
            {
                Debug.Log("Level2Locked should be false now.");
                PlayerPrefs.SetString("Level2Locked", "false");
                Debug.Log("Value of level locked now is :" + PlayerPrefs.GetString("Level2Locked"));
            }
            //1 minute cutoff to unlock level 3
            else if ( Application.loadedLevelName.Equals("level2"))
            {
                PlayerPrefs.SetString("Level3Locked", "false");
            }
            //2 minutes cutoff to unlock level 4
            else if (Application.loadedLevelName.Equals("level3"))
            {
                PlayerPrefs.SetString("Level4Locked", "false");
            }

            PlayerPrefs.Save();



            PlaySound (0);
			pauseMenu.disabled = true;
			isPaused = true;
			endMenu.display ();
			endMenuCanvas.SetActive (true);
		}
	}

	void PlaySound (int clip) {
		audio.clip = audioClip [clip];
		audio.Play ();
	}
}
