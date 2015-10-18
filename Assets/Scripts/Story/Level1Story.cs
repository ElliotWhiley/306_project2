using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Level1Story : MonoBehaviour {
	
	public Text flubText;
	public Text evilText;
	private int count = -1;
	//public Text btnText;
	private int countIntro = 0;
	ButtonText contButton;
	
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
			setFlubText ("Flubba: What is this place?");
			countIntro++;
		} else if (countIntro == 1) {
			setFlubText ("");
			setEvilText ("Voice: This is Elon Bohr's chemistry lab");
			countIntro++;

		} else if (countIntro == 2) {
			setEvilText ("Voice: He created you to sabotage those who oppose him.");
			countIntro++;

		} else if (countIntro == 3) {
			setEvilText ("");
			setFlubText ("Flubba: Then how come I'm talking to you?");
			countIntro++;
		} else if (countIntro == 4) {
			//btnText.text = "";
			setFlubText ("");
			setEvilText ("Voice: Because I broke in and freed you after your creation.");
			countIntro++;

		} else if (countIntro == 5) {
			setEvilText ("Voice: Right now there's nothing for you to do but keep moving.");
			countIntro++;

		}else if (countIntro == 6){
			Application.LoadLevel("level2");
		}
	}


	public void skipped(){
		Application.LoadLevel("level2");
	}
}
