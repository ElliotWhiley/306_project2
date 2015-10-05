using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelperText : MonoBehaviour {

	public Text text;
	
	// Use this for initialization
	void Start () {
		
		text.text = "Find your way out of the lab as fast as you can!";
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}

	public void setText(string msg){
		text.text = msg;
	}
}
