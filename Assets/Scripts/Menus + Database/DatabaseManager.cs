using UnityEngine;
using System.Collections;
using System;

public class DatabaseManager : MonoBehaviour {

    public GameObject ProgressCircle;

    public void PostScore(string level, string name, string time)
    {
        ProgressCircle.SetActive(true);

        string url = "https://microsoft-apiapp72ef49a46b6242d28d294f2cda80c2cf.azurewebsites.net/api/Values";
        WWWForm form = new WWWForm();
        form.AddField("level", level);
        form.AddField("name", name);
        form.AddField("time", time);
        Guid g = Guid.NewGuid();
        form.AddField("guid", g.ToString());

        string url2 = "https://microsoft-apiapp72ef49a46b6242d28d294f2cda80c2cf.azurewebsites.net/api/Replays";
        WWWForm form2 = new WWWForm();
        form2.AddField("fileid", g.ToString());
        form2.AddField("content", PlayerPrefs.GetString("replayString"));

        WWW www = new WWW(url, form);
        WWW www2 = new WWW(url2, form2);

        StartCoroutine(WaitForRequest(www, www2));
    }

    IEnumerator WaitForRequest(WWW www, WWW www2)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
            StartCoroutine(WaitForRequest2(www2));
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
            ProgressCircle.SetActive(false);
        }
    }

    IEnumerator WaitForRequest2(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!(2): " + www.text);
        }
        else
        {
            Debug.Log("WWW Error(2): " + www.error);
        }
        ProgressCircle.SetActive(false);
    }
}
