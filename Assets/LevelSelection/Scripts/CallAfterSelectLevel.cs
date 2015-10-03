using UnityEngine;
using System.Collections;

public class CallAfterSelectLevel : MonoBehaviour
{
    public void CallAfterSelectLevelExample(int levelIndex)
    {
        Debug.Log("Load Level " + (levelIndex + 1) + " (Level Index on LevelSelection component = " + levelIndex + ")");
    }
}
