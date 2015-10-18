using UnityEngine;
using System.Collections;

public class Level1Helper : MonoBehaviour {

	public HelperText helperText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D(Collider2D other){
		if (gameObject.tag == "Helper1") {
			helperText.setText("Press spacebar to jump.");
		} else if (gameObject.tag == "Helper2") {
			helperText.setText("Press spacebar again while in the air to double jump.");
		} else if (gameObject.tag == "Helper3") {
			helperText.setText("Watch out for dangerous lab equipment!");
		} else if (gameObject.tag == "Helper4") {
			helperText.setText("Collect coins to subtract 1 second from your time, and to unlock new skins!");
		} else if (gameObject.tag == "Helper5") {
			helperText.setText("The faster you complete the levels, the higher your chances of unlocking a coin jackpot!");
		} else if (gameObject.tag == "Helper6") {
			helperText.setText("You can stick to walls temporarily and jump between them to 'wall jump'.");
		}
	}
}
