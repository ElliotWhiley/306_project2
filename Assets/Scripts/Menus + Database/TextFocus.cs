using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFocus : MonoBehaviour {

    public Text name;

    void ClearText (bool focussed) { 
        if (focussed) {
            name.text = "";
        }
    }
}
