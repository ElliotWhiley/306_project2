using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour {


    public GameObject canvas;

    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;

    void Start()
    {
        Debug.Log("Level2Locked is currently :" + PlayerPrefs.GetString("Level2Locked"));
        Debug.Log("Level3Locked is currently :" + PlayerPrefs.GetString("Level3Locked"));
        Debug.Log("Level4Locked is currently :" + PlayerPrefs.GetString("Level4Locked"));

        if (PlayerPrefs.GetString("Level2Locked").Equals("false"))
        {
            Debug.Log("level2button should be interactable");
            level2Button.interactable = true;
            //level2Button.image.color = new Color32(29, 211, 20, 255); //green colour (level enabled).

        }
        else
        {
            Debug.Log("level2button should NOT be interactable");
            level2Button.interactable = false;

        }

        if (PlayerPrefs.GetString("Level3Locked").Equals("false"))
        {
            Debug.Log("level3button interactability is : " + level3Button.interactable);
            level3Button.interactable = true;
            //level3Button.image.color = new Color32(29, 211, 20, 255); //green colour (level enabled).
        }
        else
        {
            Debug.Log("level3button interactability is : " + level3Button.interactable);
            level3Button.interactable = false;
        }

        if (PlayerPrefs.GetString("Level4Locked").Equals("false"))
        {
            level4Button.interactable = true;
            //level4Button.image.color = new Color32(29, 211, 20, 255); //green colour (level enabled).
        }
        else
        {
            level4Button.interactable = false;
        }



    }
    // Update is called once per frame
    void Update () {
	
	}
}
