using UnityEngine;
using System.Collections;
using System;

public class LevelSelection : MonoBehaviour
{
    public enum HitAnim { None, Random, ShakeAnim, MoveAnim, ShakeAndMoveAnim };
    private Array HitAnimValues;

    public Transform levelButtonsParent;
    public LevelButton[] levels;
    private Transform levelButtonClone;

    public bool drawLinesBetweenLevels = true;
    public Transform linePrefab;
    public Transform linePrefabGrey;
    private Transform lineClone;
    public bool resetLineScale = true;
    private LineRenderer lineRendererArrow;

    private Shader initialShader;
    public Shader greyscaleShader;

    public Transform[] starsPrefabs = new Transform[2];
    private Transform starClone;
    public bool displayStateStars = true;
    
    private int levelIndex = 0;
    private int i = 0;

    public Vector3[] starsLocalPositions = new Vector3[3];
    public Vector3[] starsLocalScale = new Vector3[3];
    public bool addStarsRelativeToButtonPosition = true;

    public HitAnim buttonHitAnim = HitAnim.ShakeAndMoveAnim;
    private HitAnim buttonHitRandomAnim;
    private System.Random random = new System.Random();

    public CallMethod callMethodAfterSelectLevel;

    private ArrayList objectsCreated = new ArrayList();
    public bool destroyAllObjsCreatedOnDestroy = true;

    private bool levelSelected = false;

    public bool playSoundOnHit = false;

	void Start()
    {
        HitAnimValues = Enum.GetValues(typeof(HitAnim));

        CreateButtons();
	}

    void CreateButtons()
    {
        objectsCreated.Clear();

        //iterate throught all levels
        for (levelIndex = 0; levelIndex < levels.Length; levelIndex++)
        {
            //instantiate level prefab
            levelButtonClone = Instantiate(levels[levelIndex].prefab) as Transform;
            levelButtonClone.gameObject.name = "SelectLevel-" + levelIndex;

            //set level prefab scale
            levelButtonClone.localScale = levels[levelIndex].scale;

            //set level prefab parrent if there is any
            if (levelButtonsParent)
            {
                levelButtonClone.parent = levelButtonsParent;
            }

            //set level prefab position (local or global)
            if (levels[levelIndex].isLocalPosition)
            {
                levelButtonClone.localPosition = levels[levelIndex].position;
            }
            else
            {
                levelButtonClone.position = levels[levelIndex].position;
            }

            //set level prefab eulerAngles (local or global)
            if (levels[levelIndex].isLocalEulerAngles)
            {
                levelButtonClone.localEulerAngles = levels[levelIndex].eulerAngles;
            }
            else
            {
                levelButtonClone.eulerAngles = levels[levelIndex].eulerAngles;
            }

            //set texture
            if (levels[levelIndex].state < 0)
            {
                levelButtonClone.GetComponent<Renderer>().material.shader = greyscaleShader;
            }

            //add button created to created objects list
            objectsCreated.Add(levelButtonClone);

            //set line between two level buttons
            if (drawLinesBetweenLevels && levelIndex > 0)
            {
                if (levels[levelIndex].state < 0)
                {
                    lineClone = Instantiate(linePrefabGrey) as Transform;
                }
                else
                {
                    lineClone = Instantiate(linePrefab) as Transform;
                }

                if (levelButtonsParent)
                {
                    lineClone.parent = levelButtonsParent;
                }

                //reset local position, local euler angles and local scale
                lineClone.localPosition = Vector3.zero;
                lineClone.localEulerAngles = Vector3.zero;
                if (resetLineScale)
                {
                    lineClone.localScale = Vector3.one;
                }

                //set line renderer positions (start position and end position)
                lineRendererArrow = lineClone.GetComponent<LineRenderer>();
                lineRendererArrow.SetPosition(0, levels[levelIndex - 1].position);
                lineRendererArrow.SetPosition(1, levels[levelIndex].position);

                //add line created to created objects list
                objectsCreated.Add(lineClone);
            }

            //set stars
            if (displayStateStars)
            {
                if (levels[levelIndex].state >= 0)
                {
                    for (i = 0; i < starsLocalPositions.Length; i++)
                    {
                        if (i < levels[levelIndex].state)       //full star
                        {
                            starClone = Instantiate(starsPrefabs[1]) as Transform;
                        }
                        else        //empty star
                        {
                            starClone = Instantiate(starsPrefabs[0]) as Transform;
                        }

                        //set star parent
                        if (addStarsRelativeToButtonPosition)
                        {
                            starClone.parent = levelButtonClone;
                        }

                        //set star local position and local scale
                        starClone.localPosition = starsLocalPositions[i];
                        starClone.localEulerAngles = Vector3.zero;
                        starClone.localScale = starsLocalScale[i];

                        //add star created to created objects list
                        objectsCreated.Add(starClone);
                    }
                }
            }
        }
    }

    private Ray ray;
    private RaycastHit hit;
    private Touch touch;

    void LateUpdate()
    {
        if (!levelSelected)
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (Input.touchCount > 0)
                {
                    touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Ended)
                    {
                        ray = Camera.main.ScreenPointToRay(touch.position);
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.collider.gameObject.GetComponent<Renderer>().material.shader != greyscaleShader)
                            {
                                if (buttonHitAnim != HitAnim.None)
                                {
                                    if (buttonHitAnim != HitAnim.Random)
                                    {
                                        buttonHitRandomAnim = buttonHitAnim;
                                    }
                                    else
                                    {
                                        buttonHitRandomAnim = (HitAnim)HitAnimValues.GetValue(random.Next(2, HitAnimValues.Length));
                                    }

                                    hit.collider.gameObject.SendMessage(buttonHitRandomAnim.ToString(), SendMessageOptions.DontRequireReceiver);
                                }

                                if (playSoundOnHit)
                                {
                                    GetComponent<AudioSource>().Play();
                                }

                                levelSelected = true;

                                CallAfterSelectLevel(hit.collider.gameObject.name.Split('-')[1]);
                            }
                        }
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.GetComponent<Renderer>().material.shader != greyscaleShader)
                        {
                            if (buttonHitAnim != HitAnim.None)
                            {
                                if (buttonHitAnim != HitAnim.Random)
                                {
                                    buttonHitRandomAnim = buttonHitAnim;
                                }
                                else
                                {
                                    buttonHitRandomAnim = (HitAnim)HitAnimValues.GetValue(random.Next(2, HitAnimValues.Length));
                                }

                                hit.collider.gameObject.SendMessage(buttonHitRandomAnim.ToString(), SendMessageOptions.DontRequireReceiver);
                            }

                            if (playSoundOnHit)
                            {
                                GetComponent<AudioSource>().Play();
                            }

                            levelSelected = true;

                            CallAfterSelectLevel(hit.collider.gameObject.name.Split('-')[1]);
                        }
                    }
                }
            }
        }
    }

    public LevelButton GetButton(int index)
    {
        if (index >= 0 && index <= levels.Length - 1)
        {
            return levels[index];
        }
        Debug.LogWarning("Button index out of bounds!");
        return null;
    }

    public void SetButton(int index, LevelButton newButton)
    {
        if (index >= 0 && index <= levels.Length - 1)
        {
            levels[index] = newButton;

            DestroyAll();
            CreateButtons();
        }
        else
        {
            Debug.LogWarning("Button index out of bounds!");
        }
    }

    public void AddButton(LevelButton newButton, int index = -1)
    {
        Array.Resize<LevelButton>(ref levels, levels.Length + 1);
        if (index < 0 || index >= levels.Length)
        {
            levels[levels.Length - 1] = newButton;
        }
        else
        {
            for (int i = levels.Length - 1; i > index; i--)
            {
                levels[i] = levels[i - 1];
            }
            levels[index] = newButton;
        }

        DestroyAll();
        CreateButtons();
    }

    public void SetButtonState(int index, int state)
    {
        if (state < 0)
        {
            state = 0;
        }
        else if (state > 3)
        {
            state = 3;
        }

        if (levels[index].state != state)
        {
            levels[index].state = state;
            DestroyAll();
            CreateButtons();
        }
    }

    public void CallAfterSelectLevel(string levelIndex)
    {
        if (callMethodAfterSelectLevel.call)
        {
            if (callMethodAfterSelectLevel.sendLevelNr)
            {
                StartCoroutine(CallMethodWithDelay(callMethodAfterSelectLevel.callDealy, callMethodAfterSelectLevel.objectName, callMethodAfterSelectLevel.methodName, int.Parse(levelIndex)));
            }
            else
            {
                StartCoroutine(CallMethodWithDelay(callMethodAfterSelectLevel.callDealy, callMethodAfterSelectLevel.objectName, callMethodAfterSelectLevel.methodName));
            }
        }
    }

    IEnumerator CallMethodWithDelay(float delay, string gameObectName, string methodName, object sendValue = null)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

        if (sendValue == null)
        {
            GameObject.Find(gameObectName).SendMessage(methodName, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            GameObject.Find(gameObectName).SendMessage(methodName, sendValue, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void DestroyAll()
    {
        foreach (Transform objCreated in objectsCreated)
        {
            Destroy(objCreated.gameObject);
        }

        objectsCreated.Clear();
        System.GC.Collect();
    }

    void OnDestroy()
    {
        if (destroyAllObjsCreatedOnDestroy)
        {
            DestroyAll();
        }
    }
}
