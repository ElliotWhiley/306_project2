using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [HideInInspector]
    public bool facingRight = true;

    [HideInInspector]
    public bool jump = true;

    [HideInInspector]
    public int airCharge;

    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;
    public Transform rightWallCheck;
    public Transform leftWallCheck;

    private bool grounded = false;
    private bool rightWalled = false;
    private bool leftWalled = false;
    private Animator anim;
    private Rigidbody2D rb2d;


    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        airCharge = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb2d.angularVelocity = 0;

        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Platform"));
        rightWalled = Physics2D.Linecast(transform.position, rightWallCheck.position, 1 << LayerMask.NameToLayer("Platform"));
        leftWalled = Physics2D.Linecast(transform.position, leftWallCheck.position, 1 << LayerMask.NameToLayer("Platform"));
        if (grounded)
        {
            airCharge = 1;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if(grounded || airCharge >= 0)
            {
                if (!grounded)
                {
                    airCharge--;
                }
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(new Vector2(0, jumpForce));
                airCharge--;

                
            } else if((rightWalled || leftWalled) && airCharge >= 0)
            {
                rb2d.velocity = new Vector2(0, 0);
                if(rightWalled)
                {
                    rb2d.AddForce(new Vector2(-jumpForce/2, jumpForce/2));
                } else if(leftWalled)
                {
                    rb2d.AddForce(new Vector2(jumpForce/2, jumpForce/2));
                }
               
                airCharge--;
            }
        }
        
      
       
       

        float h = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed)
        {
            rb2d.AddForce(Vector2.right * h * moveForce);
        }

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
        {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        }

        if (h > 0 && !facingRight)
        {
            Flip();
        }
        else if (h < 0 && facingRight)
        {
            Flip();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
