using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardPopulator : MonoBehaviour {

    public GameObject ProgressCircle;

    public CanvasLevel CL1;
    public CanvasLevel CL2;
    public CanvasLevel CL3;
    public CanvasLevel CL4;
    public List<CanvasLevel> CList;

    public TimeEntry[,] TList2D;
    

    // Use this for initialization
    void Start()
    {
        CList = new List<CanvasLevel>();
        TList2D = new TimeEntry[8, 4];

        CList.Add(CL1);
        CList.Add(CL2);
        CList.Add(CL3);
        CList.Add(CL4);

        for (int i = 0; i < 4; i++)    //change i to 4 later
        {
            for (int j = 0; j < 8; j++)
            {
                TList2D[j, i] = CList[i].TList[j];
            }
        }

        string url = "https://microsoft-apiapp72ef49a46b6242d28d294f2cda80c2cf.azurewebsites.net/api/Values/1";
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www, 1));
    
        string url2 = "https://microsoft-apiapp72ef49a46b6242d28d294f2cda80c2cf.azurewebsites.net/api/Values/2";
        WWW www2 = new WWW(url2);
        StartCoroutine(WaitForRequest(www2, 2));

        string url3 = "https://microsoft-apiapp72ef49a46b6242d28d294f2cda80c2cf.azurewebsites.net/api/Values/3";
        WWW www3 = new WWW(url3);
        StartCoroutine(WaitForRequest(www3, 3));

        string url4 = "https://microsoft-apiapp72ef49a46b6242d28d294f2cda80c2cf.azurewebsites.net/api/Values/4";
        WWW www4 = new WWW(url4);
        StartCoroutine(WaitForRequest(www4, 4));

    }

    IEnumerator WaitForRequest(WWW www, int levelNumber)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
            Debug.Log("Fetched: " + www.text);
            JSONObject obj = new JSONObject(www.text);

            for (int j = 0; j < obj.list.Count; j++)
            {
                string name = obj.list[j].list[0].str;
                string time = obj.list[j].list[1].str;

                TList2D[j, levelNumber - 1].Player.text = name;
                TList2D[j, levelNumber - 1].Time.text = time;
            }
            ProgressCircle.SetActive(false);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}
