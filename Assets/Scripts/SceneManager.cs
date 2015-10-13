using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	// Changes current focus to specified scene (find scene by number under file -> Build Settings in Unity)
	public void ChangeToScene (string sceneToChangeTo) {

        if (!PlayerPrefs.GetString("Level2Locked").Equals("false") && sceneToChangeTo.Equals("level2"))
        {
            return;
        }
        else if (!PlayerPrefs.GetString("Level3Locked").Equals("false") && sceneToChangeTo.Equals("level3"))
        {
            return;
        }
        else if (!PlayerPrefs.GetString("Level4Locked").Equals("false") && sceneToChangeTo.Equals("level4"))
        {
            return;
        }


        Application.LoadLevel (sceneToChangeTo);
	}

	// Exits the game
	public void QuitGame () {
		Application.Quit ();
	}
}
