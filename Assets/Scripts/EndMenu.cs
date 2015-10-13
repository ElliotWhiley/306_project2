﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndMenu : MonoBehaviour {

	public GameObject endMenuCanvas;
	public Text coinText;
	public Text timeText;
    public Text playerName;
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
		
		coinText.text = numCoin.ToString () + "/" + coins.Length.ToString();
		timeText.text = finalTime;
	}
    
	public void backToMainMenu () {
		Application.LoadLevel ("main_menu");
	}

	public void replayTheStage () {
        Application.LoadLevel(Application.loadedLevelName);
    }

	public void continueWithNext () {
		Application.LoadLevel ("level_select");
	}

    public void uploadScore()
    {
        Debug.Log("Uploading Score..."); //need a better way of displaying upload is complete.
        dbManager.PostScore("1", playerName.text, timeText.text);
        Debug.Log("Upload complete!");
    }
}
