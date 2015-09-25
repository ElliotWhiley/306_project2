using UnityEngine;
using System.Collections;

public class PlatformPlayerController : MonoBehaviour {

    private Rigidbody2D rb;
    private int jumpCount = 0;

    [SerializeField]
    KeyCode jumpKey;
    [SerializeField]
    float speed;
    [SerializeField]
    float jump_vel;

    //Initialization before first update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

	//Initialization on first update
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = rb.velocity.y;

        if (Input.GetKeyDown(jumpKey) && jumpCount < 2)
        {
            y = jump_vel;
            jumpCount++;
        }

        rb.velocity = new Vector2(speed * x, y);
        rb.transform.rotation = Quaternion.Euler(0,0,0);
	}

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Ground")
        {
            jumpCount = 0;
        }
    }
}
