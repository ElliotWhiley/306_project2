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
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www));

        Debug.Log(form.data);
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
        ProgressCircle.SetActive(false);
    }
}
