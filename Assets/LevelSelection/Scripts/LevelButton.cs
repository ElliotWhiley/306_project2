using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelButton
{
    public Transform prefab;

    public Vector3 position = Vector3.zero;
    public bool isLocalPosition = true;

    public Vector3 eulerAngles = Vector3.zero;
    public bool isLocalEulerAngles = true;

    public Vector3 scale = Vector3.one;

    public int state = 0;
}
