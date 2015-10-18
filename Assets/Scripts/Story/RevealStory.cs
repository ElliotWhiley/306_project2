using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RevealStory : MonoBehaviour {


	public Text elonText;
	public Text flbText;
	//public Text btnText;
	private int countIntro = 0;
	public Image myImage;
	
	public void Update(){
		if (Input.GetKeyDown("return")){
			//countIntro++;

			clicked();
		}
	}
	
	void setElonText(string msg){
		elonText.text = msg;
	}
	
	void setFlubText(string msg){
		flbText.text = msg;
	}
	
	public void clicked(){
		if (countIntro == 0) {
			setFlubText ("Flubba: What have I done??");
			countIntro++;
		} else if (countIntro == 1) {
			setFlubText ("");
			setElonText ("Elon: This is your creators fault.");
			countIntro++;
		} else if (countIntro == 2) {
			setFlubText ("");
			setElonText ("Elon: You must stop him.");
			countIntro++;

		} else if (countIntro == 3) {
			setElonText("");
			setFlubText ("Flubba: Where can I find him?");
			countIntro++;

		} else if (countIntro == 4) {
			setFlubText ("");
			setElonText ("Elon: He's inside this physics lab, but be careful!");
			countIntro++;
		} else if (countIntro == 5) {
			setFlubText ("");
			setElonText ("Elon: This is our last chance Flubba, good luck.");
			countIntro++;

		} else if (countIntro == 6) {
			Application.LoadLevel("end_physics");
		}
	}
	
	public void skipped(){
		Application.LoadLevel("level4");
	}
}
