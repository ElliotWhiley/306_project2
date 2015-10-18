using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	public GameObject startPoint;
	public DeathCount deathCount;
	public GameObject currentCheckpoint;
	public AudioClip deathAudioClip;

	public GameObject green;
	public GameObject lightblue;
	public GameObject blue;
	public GameObject red;
	public GameObject orange;
	public GameObject purple;

	private AudioSource audio;
	private GameObject currentFlubba;
	private GameObject flubba;

	void Awake () {

		Debug.Log (PlayerPrefs.GetInt ("Current Skin"));

		switch (PlayerPrefs.GetInt ("Current Skin")) {
		case 1:
			flubba = green;
			break;
		case 2:
			flubba = blue;
			break;
		case 3:
			flubba = lightblue;
			break;
		case 4:
			flubba = orange;
			break;
		case 5:
			flubba = purple;
			break;
		case 6:
			flubba = red;
			break;
		default:
			flubba = green;
			break;
		}
		
		currentCheckpoint = startPoint;
		Instantiate (flubba);
	}
	
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		currentFlubba = GameObject.FindWithTag ("Player");
		RespawnPlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void RespawnPlayer () {
		currentFlubba.transform.position = currentCheckpoint.transform.position;
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			HandleDeath ();
		}
		//Debug.Log ("object left the game area");
	}

	public void HandleDeath () {
		audio.clip = deathAudioClip;
		audio.Play ();
		deathCount.deathCount++;
		RespawnPlayer ();
	}
}
