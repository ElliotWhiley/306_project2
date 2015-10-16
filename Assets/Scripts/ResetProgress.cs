﻿using UnityEngine;
using System.Collections;

public class ResetProgress : MonoBehaviour {

	// Use this for initialization
	void Start () {

        PlayerPrefs.SetString("Level2Locked", "true");
        PlayerPrefs.SetString("Level3Locked", "true");
        PlayerPrefs.SetString("Level4Locked", "true");
        PlayerPrefs.SetFloat("level1Best", 0);
        PlayerPrefs.SetFloat("level2Best", 0);
        PlayerPrefs.SetFloat("level3Best", 0);
        PlayerPrefs.SetFloat("level4Best", 0);

        PlayerPrefs.Save();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
