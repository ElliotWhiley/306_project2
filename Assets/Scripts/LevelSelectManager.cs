using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour {

    string level2Locked;
    string level3Locked;
    string level4Locked;
    public GameObject canvas;

    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;

    // Use this for initialization
    void Start () {
        level2Locked = PlayerPrefs.GetString("level2Locked");
        // If level 2 is unlocked, allow user to click level 2
        if (level2Locked.Equals("false")) {
            level2Button.interactable = true;
        }

        level3Locked = PlayerPrefs.GetString("level3Locked");
        // If level 3 is unlocked, allow user to click level 3
        if (level3Locked.Equals("false")) {
            level3Button.interactable = true;
        }

        level4Locked = PlayerPrefs.GetString("level4Locked");
        // If level 4 is unlocked, allow user to click level 4
        if (level4Locked.Equals("false")) {
            level4Button.interactable = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
