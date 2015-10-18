using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroLab : MonoBehaviour {
	
	public Text text;
	public Text flubText;
	public Text evilText;
	private int count = 0;
	//public Text btnText;
	private int countIntro = -1;
	ButtonText contButton;
	//Fader fader;
	
	// Use this for initialization
	void Start () {
		setText("\"Knife..\"");
		//setFlubText("Flubba: Huh?");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("return")){
			//count++;
			clicked();
		}
	}
	
	public void setText(string msg){
		text.text = msg;
	}
	
	public void setFlubText(string msg){
		flubText.text = msg;
	}
	
	public void setEvilText(string msg){
		evilText.text = msg;
	}
	
	public void clicked(){
		if (count == 0) {
			setText ("\"Scalpel..\"");
			count++;
		} else if (count == 1) {
			setText ("\"Sponge..\"");
			count++;
		} else if (count == 2) {
			setText ("\"Wait! It's convulsing!\"");
			count++;
		} else if (count == 3) {
			setText ("\"Grab the shotgun! Grab the shotgun!\"");
			count++;
		} else if (count == 4) {
			//Fader fader;
			//fader.EndScene();
			Application.LoadLevel ("intro_tutorial2");
		}
		
	}

	public void skipped(){
		Application.LoadLevel("level1");
	}
}
