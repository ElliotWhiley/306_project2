using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{

    public GameObject player;

    public GameObject SkinCanvas1;
    public GameObject SkinCanvas2;
    public GameObject SkinCanvas3;
    public GameObject SkinCanvas4;
    public GameObject SkinCanvas5;
    public GameObject SkinCanvas6;
    public GameObject RandomCanvas;

    public Text CoinCount1;
    public Text CoinCount2;
    public Text CoinCount3;
    public Text CoinCount4;
    public Text CoinCount5;
    public Text CoinCount6;
    public Text CoinCountRandom;

    public Sprite Skin1;
    public Sprite Skin2;
    public Sprite Skin3;
    public Sprite Skin4;
    public Sprite Skin5;
    public Sprite Skin6;

    public AnimationClip anim1;
    public AnimationClip anim2;
    public AnimationClip anim3;
    public AnimationClip anim4;
    public AnimationClip anim5;
    public AnimationClip anim6;

    public Button applyButton1;
    public Button applyButton2;
    public Button applyButton3;
    public Button applyButton4;
    public Button applyButton5;
    public Button applyButton6;
    public Button applyButtonRandom;

    void Start()
    {
        PlayerPrefs.SetInt("Green Slime", 1);
        PlayerPrefs.SetInt("Current Skin", 0);
        PlayerPrefs.SetInt("Random Select", 0);
        CoinCount1.text = PlayerPrefs.GetInt("coinCount").ToString();
        conditionalOnAndOff();
        //player.GetComponent<Animation>().AddClip(anim1, "greenAnim");
        //player.GetComponent<Animation>().AddClip(anim2, "blueAnim");
        //player.GetComponent<Animation>().AddClip(anim3, "lightBlueAnim");
        //player.GetComponent<Animation>().AddClip(anim4, "orangeAnim");
        //player.GetComponent<Animation>().AddClip(anim5, "purpleAnim");
        //player.GetComponent<Animation>().AddClip(anim6, "redAnim");
    }

    public void ChangeSkinCanvas(int skinNum)
    {
        switch (skinNum)
        {
            //pressed back button from canvas 1
            case 1:
                SkinCanvas1.SetActive(false);
                RandomCanvas.SetActive(true);
                CoinCountRandom.text = PlayerPrefs.GetInt("coinCount").ToString();
                conditionalOnAndOff();
                break;

            //pressed next button from canvas 1
            case 2:
                SkinCanvas1.SetActive(false);
                SkinCanvas2.SetActive(true);
                CoinCount2.text = PlayerPrefs.GetInt("coinCount").ToString();
                conditionalOnAndOff();
                break;

            //pressed back button from canvas 2
            case 3:
                SkinCanvas2.SetActive(false);
                SkinCanvas1.SetActive(true);
                CoinCount1.text = PlayerPrefs.GetInt("coinCount").ToString();
                conditionalOnAndOff();
                break;

            //pressed next button from canvas 2
            case 4:
                SkinCanvas2.SetActive(false);
                SkinCanvas3.SetActive(true);
                CoinCount3.text = PlayerPrefs.GetInt("coinCount").ToString();
                conditionalOnAndOff();
                break;

            //pressed back button from canvas 3
            case 5:
                SkinCanvas3.SetActive(false);
                SkinCanvas2.SetActive(true);
                CoinCount2.text = PlayerPrefs.GetInt("coinCount").ToString();
                conditionalOnAndOff();
                break;

            //pressed next button from canvas 3
            case 6:
                SkinCanvas3.SetActive(false);
                SkinCanvas4.SetActive(true);
                CoinCount4.text = PlayerPrefs.GetInt("coinCount").ToString();
                conditionalOnAndOff();
                break;

            //pressed back button from canvas 4
            case 7:
                SkinCanvas4.SetActive(false);
                SkinCanvas3.SetActive(true);
                CoinCount3.text = PlayerPrefs.GetInt("coinCount").ToString();
                conditionalOnAndOff();
                break;

            //pressed next button from canvas 4
            case 8:
                SkinCanvas4.SetActive(false);
                SkinCanvas5.SetActive(true);
                CoinCount5.text = PlayerPrefs.GetInt("coinCount").ToString();
                conditionalOnAndOff();
                break;

            //pressed back button from canvas 5
            case 9:
                SkinCanvas5.SetActive(false);
                SkinCanvas4.SetActive(true);
                CoinCount4.text = PlayerPrefs.GetInt("coinCount").ToString();
                conditionalOnAndOff();
                break;

            //pressed next button from canvas 5
            case 10:
                SkinCanvas5.SetActive(false);
                SkinCanvas6.SetActive(true);
                CoinCount6.text = PlayerPrefs.GetInt("coinCount").ToString();
                conditionalOnAndOff();
                break;

            //pressed back button from canvas 6
            case 11:
                SkinCanvas6.SetActive(false);
                SkinCanvas5.SetActive(true);
                CoinCount5.text = PlayerPrefs.GetInt("coinCount").ToString();
                conditionalOnAndOff();
                break;

            //pressed next button from canvas 6
            case 12:
                SkinCanvas6.SetActive(false);
                RandomCanvas.SetActive(true);
                CoinCountRandom.text = PlayerPrefs.GetInt("coinCount").ToString();
                conditionalOnAndOff();
                break;

            //pressed back button from RandomeCanvas
            case 13:
                RandomCanvas.SetActive(false);
                SkinCanvas6.SetActive(true);
                CoinCount6.text = PlayerPrefs.GetInt("coinCount").ToString();
                conditionalOnAndOff();
                break;

            //pressed next button from RandomeCanvas
            case 14:
                RandomCanvas.SetActive(false);
                SkinCanvas1.SetActive(true);
                CoinCount1.text = PlayerPrefs.GetInt("coinCount").ToString();
                conditionalOnAndOff();
                break;
        }
    }

    public void applySkin(int skinNum)
    {
        switch (skinNum)
        {
            case 1:

                applyButtonChange("Green Slime", Skin1, anim1, applyButton1, 100, CoinCount1, skinNum);
                PlayerPrefs.SetInt("Current Skin", 1);
                break;

            case 2:
                applyButtonChange("Blue Slime", Skin2, anim2, applyButton2, 100, CoinCount2, skinNum);
                PlayerPrefs.SetInt("Current Skin", 2);
                break;

            case 3:
                applyButtonChange("Light Blue Slime", Skin3, anim3, applyButton3, 100, CoinCount3, skinNum);
                PlayerPrefs.SetInt("Current Skin", 3);
                break;

            case 4:
                applyButtonChange("Orange Slime", Skin4, anim4, applyButton4, 100, CoinCount4, skinNum);
                PlayerPrefs.SetInt("Current Skin", 4);
                break;

            case 5:
                applyButtonChange("Purple Slime", Skin5, anim5, applyButton5, 100, CoinCount5, skinNum);
                PlayerPrefs.SetInt("Current Skin", 5);
                break;

            case 6:
                applyButtonChange("Red Slime", Skin6, anim6, applyButton6, 100, CoinCount6, skinNum);
                PlayerPrefs.SetInt("Current Skin", 6);
                break;

            case 7:
                int num = Random.Range(1, 7);
                PlayerPrefs.SetInt("Random Select", 1);
                applySkin(num);
                CoinCountRandom.text = PlayerPrefs.GetInt("coinCount").ToString();
                break;
        }
    }

    void conditionalOnAndOff()
    {

        if (PlayerPrefs.GetInt("coinCount") < 50)
        {
            applyButtonRandom.interactable = false;
            applyButton1.interactable = false;
            applyButton2.interactable = false;
            applyButton3.interactable = false;
            applyButton4.interactable = false;
            applyButton5.interactable = false;
            applyButton6.interactable = false;
        }
        else
        {
            applyButtonRandom.interactable = true;
            if (PlayerPrefs.GetInt("coinCount") < 100)
            {
                applyButton1.interactable = false;
                applyButton2.interactable = false;
                applyButton3.interactable = false;
                applyButton4.interactable = false;
                applyButton5.interactable = false;
                applyButton6.interactable = false;
            }
            else
            {
                applyButton1.interactable = true;
                applyButton2.interactable = true;
                applyButton3.interactable = true;
                applyButton4.interactable = true;
                applyButton5.interactable = true;
                applyButton6.interactable = true;
            }
        }
    }

    void applyButtonChange(string intName, Sprite skin, AnimationClip anim, Button applyButton, int price, Text wallet, int skinNum)
    {
        if (PlayerPrefs.GetInt(intName) == 1)
        {
            player.GetComponent<SpriteRenderer>().sprite = skin;
            player.GetComponent<Animation>().clip = anim;
            //applyButton.GetComponentInChildren<Text>().text = "Equipped";
        }
        else
        {
            player.GetComponent<SpriteRenderer>().sprite = skin;
            player.GetComponent<Animation>().clip = anim;
            PlayerPrefs.SetInt(intName, 1);
            if (PlayerPrefs.GetInt("Random Select") == 0)
            {
                PlayerPrefs.SetInt("coinCount", PlayerPrefs.GetInt("coinCount") - price);
            }
            else
            {
                PlayerPrefs.SetInt("coinCount", PlayerPrefs.GetInt("coinCount") - price / 2);
                PlayerPrefs.SetInt("Random Select", 0);
            }
            
            //applyButton.GetComponentInChildren<Text>().text = "Equipped";
            wallet.text = PlayerPrefs.GetInt("coinCount").ToString();
        }
    }
}
