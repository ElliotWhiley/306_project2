using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeControl : MonoBehaviour {

	public Text text;

	public float time;
    public float coinCollected = 0;

	private float delay;
	private float minute;
	private float second;
	private bool playerCanMove;
    private bool stopTimer;

	// Use this for initialization
	void Start () {
		text.text = "0";
		playerCanMove = false;
        stopTimer = false;
		delay = 3;
        coinCollected = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//Delay for 3 seconds before the player can move.
		if (!playerCanMove) {
			if (Time.timeSinceLevelLoad > delay) {
				playerCanMove = true;
			} else {
				return;
			}
		}
	}

	//FixedUpdate is called once every fixed interval
	void FixedUpdate () {

		//Delay for 3 seconds before the player can move.
		if (!playerCanMove) {
			text.text = "0:00.000";
			return;
		}

        if (stopTimer == false)
        {
            time = Time.timeSinceLevelLoad - delay - coinCollected;
            minute = 0;
            second = time;

            //Get the number of minutes and seconds if the time is over a minute.
            if (second >= 60)
            {
                minute = Mathf.Floor(time / 60);
                second = time % 60;
            }

            if (time < 60)
            {
                if (time < 10)
                {
                    text.text = "0" + ":" + "0" + second.ToString("F3");
                }
                else
                {
                    text.text = "0" + ":" + second.ToString("F3");
                }
            }

            //time over a minute
            else if (time >= 60)
            {
                if (minute < 10)
                {
                    if (second < 10)
                    {
                        text.text = minute.ToString("F0") + ":" + "0" + second.ToString("F3");
                    }
                    else
                    {
                        text.text = minute.ToString("F0") + ":" + second.ToString("F3");
                    }
                }
                else
                {
                    if (second < 10)
                    {
                        text.text = minute.ToString("F0") + ":" + "0" + second.ToString("F3");
                    }
                    else
                    {
                        text.text = minute.ToString("F0") + ":" + second.ToString("F3");
                    }
                }
            }
        }
	}

    public void StopTimer()
    {
        stopTimer = true;
    }
}
