using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BioEndStory : MonoBehaviour {
	
	public Text elonText;
	public Text flbText;
	//public Text btnText;
	public Text evilText;
	private int countIntro = 0;
	public Image myImage;

	public void Update(){
		if (Input.GetKeyDown("return")){

			clicked();
			//countIntro++;

			
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
			setFlubText ("Flubba: It's over Elon. I'm getting out of this place.");
			countIntro++;
		} else if (countIntro == 1) {
			setFlubText ("");
			setElonText ("Elon: I knew this day would come, ever since your creator went into hiding!");
			countIntro++;
		} else if (countIntro == 2) {
			setElonText ("");
			setFlubText ("Flubba: Don't try and fool me.");
			countIntro++;
		} else if (countIntro == 3) {
			setFlubText ("Flubba: I know it was YOU who created me!");
			countIntro++;

		} else if (countIntro == 4) {
			setFlubText ("");
			setElonText ("Elon: How do you know that? Did the voice inside your head tell you that?!");
			countIntro++;
		} else if (countIntro == 5) {
			setElonText ("");
			setFlubText ("");
			evilText.text = "Voice: Don't listen to him, you need to kill him before he calls for help!";
			countIntro++;
		} else if (countIntro == 6) {
			setFlubText ("");
			evilText.text = "";
			setElonText ("Elon: No, I am not your creator.");
			countIntro++;

		} else if (countIntro == 7) {
			setElonText ("Elon: Your sole purpose is to cripple those who would dare stand up to your creator.");
			countIntro++;

		}else if (countIntro == 8){
			setElonText ("Elon: I have something to show you, meet me in the physics lab...");
			countIntro++;

		}else if (countIntro == 9){
			//ChangeImage("bam");
			//StartCoroutine("ChangeScene");
			Application.LoadLevel("level3");
		}
	}

	public void ChangeImage (string newImageTitle){
		myImage.sprite = Resources.Load<Sprite>(newImageTitle);
	}

	public void skipped(){
		Application.LoadLevel("level3");
	}
}
