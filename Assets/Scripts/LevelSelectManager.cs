using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelectManager : MonoBehaviour
{

    public GameObject canvas;

    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;
    



    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("Level2Locked").Equals("false"))
        {

            level2Button.image.color = new Color32(29, 211, 20, 255); //green colour (level enabled).

        }
        else
        {
            level2Button.image.color = Color.grey;
        }

        if (PlayerPrefs.GetString("Level3Locked").Equals("false"))
        {
            level3Button.image.color = new Color32(29, 211, 20, 255); //green colour (level enabled).
        }
        else
        {
            level3Button.image.color = Color.grey;
        }

        if (PlayerPrefs.GetString("Level4Locked").Equals("false"))
        {
            level4Button.image.color = new Color32(29, 211, 20, 255); //green colour (level enabled).
        }
        else
        {
            level4Button.image.color = Color.grey;
        }



    }
}
