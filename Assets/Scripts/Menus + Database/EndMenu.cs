using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

public class EndMenu : MonoBehaviour {

	public GameObject endMenuCanvas;
	public Text coinText;
	public Text timeText;
    public Text playerName;
    public Text coinsCollectedText;
    public Text bestTimeText;
    public CoinCount coinCount;
	public TimeControl timeControl;
    public DatabaseManager dbManager;

    private string finalTime;
	private int numCoin;
	private long finalScore;
	private float finalTimeInFloat;
    private GameObject[] coins;
    
    void Start() {
        coins = GameObject.FindGameObjectsWithTag("Coin");
    }

	public void display () {
		finalTime = timeControl.text.text;
		numCoin = coinCount.coinCount;

        //coinText.text = numCoin.ToString () + "/" + coins.Length.ToString();
        // Display number of coins collected for that level and total coins in wallet
        coinsCollectedText.text = "+" + numCoin.ToString();
        coinText.text = PlayerPrefs.GetInt("coinCount").ToString();

        timeText.text = finalTime;
        GetBestTime();
    }
    
	public void backToMainMenu () {
		Application.LoadLevel ("main_menu");
	}

    // Changes current focus to specified scene (find scene by number under file -> Build Settings in Unity)
    public void ChangeToScene(string sceneToChangeTo)
    {
        Application.LoadLevel(sceneToChangeTo);
    }

    public void continueWithNext () {
		Application.LoadLevel ("level_select");
	}

    public void uploadScore(string level)
    {
        Debug.Log("Uploading Score..."); 
        dbManager.PostScore(level, playerName.text, timeText.text);
        Debug.Log("Upload complete!");
    }

    public void OpenReplay()
    {
        PlayerPrefs.SetString("isReplay", "true");
        Application.LoadLevel(Application.loadedLevelName);
    }

    void GetBestTime() {
        string best;
        string currentLevel = EditorApplication.currentScene;
        // Check which level is currently in play
        switch (currentLevel) {
            case "Assets/Scenes/Levels/level1.unity":
                best = BestToString(PlayerPrefs.GetFloat("level1Best"));
                // Check that a best time has been set
                if (PlayerPrefs.GetFloat("level1Best") != 0) {
                    bestTimeText.text = best;
                }
                break;
            case "Assets/Scenes/Levels/level2.unity":
                best = BestToString(PlayerPrefs.GetFloat("level2Best"));
                // Check that a best time has been set
                if (PlayerPrefs.GetFloat("level2Best") != 0) {
                    bestTimeText.text = best;
                }
                break;
            case "Assets/Scenes/Levels/level3.unity":
                best = BestToString(PlayerPrefs.GetFloat("level3Best"));
                // Check that a best time has been set
                if (PlayerPrefs.GetFloat("level3Best") != 0) {
                    bestTimeText.text = best;
                }
                break;
            case "Assets/Scenes/Levels/level4.unity":
                best = BestToString(PlayerPrefs.GetFloat("level4Best"));
                // Check that a best time has been set
                if (PlayerPrefs.GetFloat("level4Best") != 0) {
                    bestTimeText.text = best;
                }
                break;
            default:
                break;
        }
    }

    // Converts best time taken (float) for input into a string representation of that time
    string BestToString(float bestFloat) {
        int bestMins = (int)bestFloat / 60;
        string bestSecs = (bestFloat % 60).ToString();
        bestSecs = bestSecs.Substring(0, 5);
        string bestString = bestMins + ":" + bestSecs;
        return bestString;
    }
}
