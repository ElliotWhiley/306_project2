using UnityEngine;
using System.Collections;

public class Collectables : MonoBehaviour {

	public CoinCount coinCount;
    public TimeControl timeControl;

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
            
            timeControl.time = timeControl.time - 1;
            timeControl.coinCollected++;

			coinCount.coinCount++;
			Destroy (gameObject);
		}
	}
}
