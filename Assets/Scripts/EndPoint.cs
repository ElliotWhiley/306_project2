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

            Debug.Log(endMenu.timeControl.time + "");
            Debug.Log(Application.loadedLevelName);

            //16 seconds cutoff to unlock level 2
            if (endMenu.timeControl.time < 16 && Application.loadedLevelName.Equals("level1"))
            {
                PlayerPrefs.SetString("Level2Locked", "false");
            }
            //1 minute cutoff to unlock level 3
            else if (endMenu.timeControl.time < 60 && Application.loadedLevelName.Equals("level2"))
            {
                PlayerPrefs.SetString("Level3Locked", "false");
            }
            //2 minutes cutoff to unlock level 4
            else if (endMenu.timeControl.time < 120 && Application.loadedLevelName.Equals("level3"))
            {
                PlayerPrefs.SetString("Level4Locked", "false");
            }


            PlaySound (0);
			pauseMenu.disabled = true;
			isPaused = true;
			StartCoroutine (Example ());
			endMenu.display ();
			endMenuCanvas.SetActive (true);
		}
	}

	void PlaySound (int clip) {
		audio.clip = audioClip [clip];
		audio.Play ();
	}

	IEnumerator Example () {
		yield return new WaitForSeconds (30);
	}
}
