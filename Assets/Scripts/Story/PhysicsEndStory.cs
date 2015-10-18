using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PhysicsEndStory : MonoBehaviour {

	//public Text btnText;
	public Text evilText;
	private int countIntro = 0;
	public Image myImage;
	
	public void Update(){
		if (Input.GetKeyDown("return")){
			//countIntro++;
			clicked();
		}
	}
	
	void setEvilText(string msg){
		evilText.text = msg;
	}

	
	public void clicked(){
		if (countIntro == 0) {
			setEvilText ("Creator: Flubba, you've been a good minion.");
			countIntro++;
		} else if (countIntro == 1) {
			setEvilText ("Creator: I knew when I made you, this day would come.");
			countIntro++;
		} else if (countIntro == 2) {
			setEvilText ("Creator: You think you're doing the right thing...");
			countIntro++;

		} else if (countIntro == 3) {
			setEvilText ("Creator: but it's too late. You've already done my bidding.");
			countIntro++;

		} else if (countIntro == 4) {
			setEvilText ("Creator: Do what you have to do.");
			countIntro++;
		}else if (countIntro == 5){
			Application.LoadLevel("level4");
		}
	}
	
	public void ChangeImage (string newImageTitle){
		myImage.sprite = Resources.Load<Sprite>(newImageTitle);
	}
	
	public void skipped(){
		Application.LoadLevel("level4");
	}
}
