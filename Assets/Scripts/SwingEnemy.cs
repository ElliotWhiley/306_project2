using UnityEngine;
using System.Collections;

public class SwingEnemy : MonoBehaviour {

    public bool goingRight;
    public float currentRotationZ;
    public float newRotationZ;
    public float swingVelocity;

    void Awake()
    {
        goingRight = true;
        swingVelocity = 0;
    }

	// Use this for initialization
	void Start () {
        InvokeRepeating("Swing",3,0.1F);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Swing()
    {
        currentRotationZ = transform.rotation.eulerAngles.z;
        if (goingRight)
        {
            if (currentRotationZ >= 305 && currentRotationZ < 360)
            {
                Accelerate();
            } 
            if (currentRotationZ < 50 && currentRotationZ >= 0)
            {
                Decelerate();
            }
            if (currentRotationZ > 50 && currentRotationZ < 60)
            {
                swingVelocity = 0;
                goingRight = false;
            }
            newRotationZ = currentRotationZ + swingVelocity;
            transform.rotation = Quaternion.Euler(0, 0, newRotationZ);
        }
        else
        {
            if (currentRotationZ >= 310 && currentRotationZ < 360)
            {
                Decelerate();
            }
            if (currentRotationZ < 55 && currentRotationZ >= 0)
            {
                Accelerate();
            }
            if (currentRotationZ < 311 && currentRotationZ > 300)
            {
                swingVelocity = 0;
                goingRight = true;
            }
            newRotationZ = currentRotationZ - swingVelocity;
            transform.rotation = Quaternion.Euler(0, 0, newRotationZ);
        }
    }

    void Accelerate()
    {
        swingVelocity = swingVelocity + 1;
    }

    void Decelerate()
    {
        swingVelocity = swingVelocity - 1;
    }
}
