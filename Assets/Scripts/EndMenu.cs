using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndMenu : MonoBehaviour {

    public GameObject endMenuCanvas;
    public Text coinText;
    public Text timeText;
    public Text scoreText;
    public CoinCount coinCount;
	public TimeControl timeControl;

    private string finalTime;
    private int numCoin;
    private long finalScore;
    private float finalTimeInFloat; 
    

    // Use this for initialization
    void Start () {
        //finalTime = countDown.text.text;
        //numCoin = coinCount.coinCount;

        //coinText.text = numCoin.ToString() + "/5";
        //timeText.text = finalTime;
        //scoreText.text = "10000";
    }

	public void display(){
		finalTime = timeControl.text.text;
		numCoin = coinCount.coinCount;
		
		coinText.text = numCoin.ToString() + "/5";
		timeText.text = finalTime;
		scoreText.text = "10000";
	}
    
    public void backToMainMenu()
    {
        Application.LoadLevel("main_menu");
    }

    public void replayTheStage()
    {
        Application.LoadLevel("level1");
    }

    public void continueWithNext()
    {
        Application.LoadLevel("level_select");
    }
}
