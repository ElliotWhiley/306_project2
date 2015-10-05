using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public bool isPaused;
	public bool disabled;
	public GameObject pauseMenuCanvas;



	// Update is called once per frame
	void Update () {
		if (isPaused) {
			pauseMenuCanvas.SetActive (true);
			Time.timeScale = 0f;
		} else {
			pauseMenuCanvas.SetActive (false);
			Time.timeScale = 1f;
		}
			
		if (Input.GetKeyDown(KeyCode.Escape) && (disabled == false)){
			isPaused = !isPaused;
		}
	}

	public void resume(){
		isPaused = !isPaused;
	}

	public void backToMainMenu(){
		Application.LoadLevel("main_menu");
	}
}
