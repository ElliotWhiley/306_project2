using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeEntry : MonoBehaviour {

    public Text Player;
    public Text Time;
    public string guid;
    public GameObject ProgressCircle;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setPlayer(string x)
    {
        Player.text = x;
    }

    public void setTIme(string y)
    {
        Time.text = y;
    }

    public void playReplay()
    {
        Debug.Log("timelist guid: " + guid);

        ProgressCircle.SetActive(true);

        string url = "https://microsoft-apiapp72ef49a46b6242d28d294f2cda80c2cf.azurewebsites.net/api/Replays?fileid="+guid;
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));

    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);

            ProgressCircle.SetActive(false);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
            ProgressCircle.SetActive(false);
        }
    }
}
