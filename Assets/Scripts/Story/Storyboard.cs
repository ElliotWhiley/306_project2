using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Storyboard : MonoBehaviour {

	public Text text;
	public Text flubText;
	public Text evilText;
	//public Text btnText;
	private int countIntro = 0;
	ButtonText contButton;
	Fader fader;

	// Use this for initialization
	void Start () {
		//setFlubText("Flubba: Huh?");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("return")){
			//countIntro++;

			clickedIntro();
		}
	}

	public void setFlubText(string msg){
		flubText.text = msg;
	}

	public void setEvilText(string msg){
		evilText.text = msg;
	}

	public void clickedIntro(){
		if (countIntro == 0) {
			setFlubText ("Flubba: Huh? Where am I?");
			countIntro++;

		} else if (countIntro == 1) {
			setFlubText ("");
			setEvilText ("Voice: Thank god! You made it!");
			countIntro++;

		} else if (countIntro == 2) {
			setEvilText ("Voice: Things were looking pretty hairy for a while back there.");
			countIntro++;

		} else if (countIntro == 3) {
			setEvilText ("");
			setFlubText ("Flubba: What? Who are you?");
			countIntro++;

		} else if (countIntro == 4) {
			setFlubText ("");
			setEvilText ("Voice: That doesn't matter right now.");
			countIntro++;

		} else if (countIntro == 5) {
			setEvilText ("Voice: Right now you just need to get out of here.");
			countIntro++;

		} else if (countIntro == 6) {
			setEvilText ("Voice: Don't worry, I'll help you.");
			countIntro++;

		}else if (countIntro == 7){
			Application.LoadLevel("level1");
		}
	}
	

	public void skipped(){
		Application.LoadLevel("level1");
	}
}
