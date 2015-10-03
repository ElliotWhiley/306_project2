using UnityEngine;
using System.Collections;

public class ShakeAndMove : MonoBehaviour
{
    public Vector3 moveDistance = new Vector3(0, 0.5f, 0);
    public Vector3 moveScaleAdd = new Vector3(0.3f, 0, 0.3f);

    public Vector3 shakeMin = Vector3.zero;
    public Vector3 shakeMax = new Vector3(0.2f, 0.2f, 0.2f);
    private Vector3 shake = Vector3.zero;

    public float animSpeed = 2;

    private bool moveAnim = false;
    private bool shakeAnim = false;
    private float animStartTime = 0;
    private float lerpTime = 0;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private Vector3 scaleStart;
    private Vector3 scaleEnd;

	void Start()
    {
        startPosition = transform.localPosition;
        scaleStart = transform.localScale;
	}
	
	void LateUpdate()
    {
        if (moveAnim || shakeAnim)
        {
            lerpTime = (Time.time - animStartTime) * animSpeed;
        }

        if (moveAnim)
        {
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, lerpTime);
            transform.localScale = Vector3.Lerp(scaleStart, scaleEnd, lerpTime);
        }

        if (shakeAnim)
        {
            shake.Set(Random.Range(shakeMin.x, shakeMax.x), Random.Range(shakeMin.y, shakeMax.y), Random.Range(shakeMin.z, shakeMax.z));
            if (moveAnim)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition + shake, lerpTime);
            }
            else
            {
                transform.localPosition = Vector3.Lerp(startPosition, startPosition + shake, lerpTime);
            }
        }

        if (moveAnim || shakeAnim)
        {
            if (lerpTime >= 1)
            {
                if (moveAnim)
                {
                    transform.localPosition = endPosition;
                    transform.localScale = scaleEnd;
                }
                else
                {
                    transform.localPosition = startPosition;
                    transform.localScale = scaleStart;
                }

                moveAnim = false;
                shakeAnim = false;
            }
        }
	}

    void ShakeAnim()
    {
        shakeAnim = true;

        animStartTime = Time.time;
    }

    void MoveAnim()
    {
        moveAnim = true;
        endPosition = startPosition + moveDistance;
        scaleEnd = scaleStart + moveScaleAdd;

        animStartTime = Time.time;
    }

    void ShakeAndMoveAnim()
    {
        shakeAnim = true;

        moveAnim = true;
        endPosition = startPosition + moveDistance;
        scaleEnd = scaleStart + moveScaleAdd;

        animStartTime = Time.time;
    }
}
