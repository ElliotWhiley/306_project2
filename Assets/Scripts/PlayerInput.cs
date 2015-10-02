using UnityEngine;
using System.Collections;

/**
 * NOTE: Uses basic ray-tracing template from https://github.com/SebLague/2DPlatformer-Tutorial/tree/master/Episode%2005
 */


[RequireComponent(typeof(PlayerPhysics))]
public class PlayerInput : MonoBehaviour 
{

    public float jumpHeight = 4;
    public float timeToJumpApex = .6f;
    float accelerationTimeAirborne = .35f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;
    float wallSlidingSpeed = 1;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    bool wallJump;
    int airCharge;

    PlayerPhysics controller;


    /**Initialisation */
    void Start() {
		//The controller is what handles our movement in the game world
        controller = GetComponent<PlayerPhysics>();

		//Gravity setup
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);

        //Player ability setup
        wallJump = false;
        airCharge = 0;
    }

	/**
	 * Update is called every frame in order to update the player with the user's input and calculate the next movment to be handled by the PlayerPhysics class.
	 * Move() takes a Vector2 argument for the amount to be moved and carries it out on the player object. While an object is colliding with a surface, the controller.collisions field
	 * will provide information to the type of collision which can be used to test for jump validity, wall jumping etc.
	 */

    void Update() {
		//Vertical collision detection. If the player touches the ground or ceiling set vertical velocity to zero.
        if (TouchingGround() || TouchingCeiling()) {
            velocity.y = 0;
            //If player lands, reset airCharge
            if (TouchingGround()) {
                airCharge = 1;
            }
        }

        //Use side collisions. Player will slide down the wall at a constant value. 
        if (TouchingWall()) {
            if (velocity.y < 0) {
                velocity.y = -wallSlidingSpeed;
                wallJump = true;
            }
        }
        else {
            wallJump = false;
        }

		//Get keyboard input
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Ignore left button if the object is on the right wall, and ignore right button if the object is on the left wall. 
        //Also, if the down button is pressed while the object is on a wall, it will slightly move the object off it 
        //and drop the object down.
        if(TouchingWall())
        {
            
            if (TouchingRightWall())
            {
                if (input.y == -1)
                {
                    velocity.x = -moveSpeed / 100;
                }
                if (input.x == -1)
                {
                    input.x = 0;
                }

            }
            else if (TouchingLeftWall())
            {
                if(input.y == -1)
                {
                    velocity.x = moveSpeed / 100;
                }
                if (input.x == 1)
                {
                    input.x = 0;
                }
            }

        }
       

        //When the jump button is pressed.
        if (Input.GetKeyDown(KeyCode.Space)) 
        {//Simply jump if the object is on the ground. 
            if(TouchingGround())
            {
                velocity.y = jumpVelocity;
            }//If the object is touching a wall, jump in the opposite direction.
            else if(TouchingWall())
            {
                if(TouchingRightWall())
                {
                    velocity.y = jumpVelocity;
                    velocity.x = -moveSpeed * (float)1.5;
                } else if(TouchingLeftWall())
                {
                    velocity.y = jumpVelocity;
                    velocity.x = moveSpeed * (float)1.5;
                }
            }
            else if(airCharge == 1)
            {
                velocity.y = jumpVelocity;
                airCharge--;
            }

        }

        /**
		//When the jump button is pressed.
        if (Input.GetKeyDown(KeyCode.Space)) {
            //If the player is on the ground, start jumping.
            if (controller.collisions.below) {
                velocity.y = jumpVelocity;
            }
            //If the player is currently on a wall.
            else if (wallJump && Mathf.Abs(input.x) == 1) {
                //Check that the jump direction is the opposite of the wall that is attached to
                if ((input.x == -1 && controller.collisions.right) || (input.x == 1 && controller.collisions.left)) {
                    velocity.y = jumpVelocity;
                    velocity.x = input.x * moveSpeed;
                }
            }
            //Double jump in air
            else if (airCharge) {
                airCharge = false;
                velocity.y = jumpVelocity;
            }
        }
        */
        float targetVelocityX = input.x * moveSpeed;
		//Player accelerates towards top speed in the direction specified by input
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        //Gravity is applied
		velocity.y += gravity * Time.deltaTime;

		//The controller is given a veloity to move the player by
        controller.Move(velocity * Time.deltaTime);
    }










    ////////////////////////////////
    /** Helper funcitons */
    /////////////////////////////////


    /**Helper function to check if the object is touching the ground or not. */
    bool TouchingGround()
    {
        if(controller.collisions.below)
        {
            return true;
        } else
        {
            return false;
        }
    }

    /**Helper function to check if the object is touching either the left or the right side walls.  */
    bool TouchingWall()
    {
        if(controller.collisions.left || controller.collisions.right)
        {
            return true;
        } else
        {
            return false;
        }
    }

    /**Helper function to check if the object is touching the right hand side wall.  */
    bool TouchingRightWall()
    {
        if(controller.collisions.right)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    /**Helper function to check if the object is touching the left hand side wall.  */
    bool TouchingLeftWall()
    {
        if(controller.collisions.left)
        {
            return true;
        } else
        {
            return false;
        }

    }

    /**Helper function to check if the object is touching the ceiling or not.  */
    bool TouchingCeiling()
    {
        if(controller.collisions.above)
        {
            return true;
        } else
        {
            return false;
        }
    }
}