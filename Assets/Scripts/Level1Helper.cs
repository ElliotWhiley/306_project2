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
			helperText.setText("Press spacebar twice rapidly to double jump.");
		} else if (gameObject.tag == "Helper3") {
			helperText.setText("Collect coins for bonus rewards!");
		} else if (gameObject.tag == "Helper4") {
			helperText.setText("Be careful not to fall! There will be penalities");
		} else if (gameObject.tag == "Helper5") {
			helperText.setText("You can stick onto walls and jump off them continuously");
		}
	}
}
