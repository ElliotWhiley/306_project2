using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathCount : MonoBehaviour {

    public int deathCount;
    //public PlayerInput playerInput;

    public Text text;

	// Use this for initialization
	void Start () {
        
        //playerInput = GetComponent<PlayerInput>();
        text.text = "Death:0";
	}
	
	// Update is called once per frame
	void Update () {
        
        text.text = "Death:" + deathCount.ToString();
	}
}
