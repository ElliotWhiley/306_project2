using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	public GameObject startPoint;
	public GameObject flubba;
	public DeathCount deathCount;
	public GameObject currentCheckpoint;

	private GameObject currentFlubba;

	void Awake () {
		currentCheckpoint = startPoint;
		Instantiate (flubba);
	}

	// Use this for initialization
	void Start () {
		currentFlubba = GameObject.Find ("Player(Clone)");
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
		deathCount.deathCount++;
		RespawnPlayer ();
	}
}
