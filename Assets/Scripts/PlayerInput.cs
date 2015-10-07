using UnityEngine;
using System.Collections;

/**
 * NOTE: Uses basic ray-tracing template from https://github.com/SebLague/2DPlatformer-Tutorial/tree/master/Episode%2005
 */

[RequireComponent(typeof(PlayerPhysics))]
[RequireComponent(typeof(LevelManager))]
public class PlayerInput : MonoBehaviour {

	//Key bindings
	public KeyCode jumpKey = KeyCode.Space;
	public KeyCode glideKey = KeyCode.LeftShift;

	//Varible defaults
	float jumpHeight = 3f;
	float timeToJumpApex = .4f;
	float accelerationTimeGrounded = .08f;
	float moveSpeed = 6f;
	float glideSpeed = 3f;
	float slidingCoefficient = 0.2f;
	float glidingFallSpeed = -2.5f;
	//Make this an enum
	float airborneAccelSlow = .35f;
	float airborneAccelFast = 0.13f;

	float gravity;
	float jumpVelocity;
	float velocityXSmoothing;
	float accelerationTimeAirborne;
	float airCharge;
	Vector3 velocity;
	bool collisionEnter;
	bool collisionContinuing;

	private float delay;

	public DeathCount deathCount;
	public int numberOfDeath;
	public AudioClip[] audioClip;
	public Animator animator;

	PlayerPhysics controller;
	AudioSource audio;
	Animation anim;

	/**Initialisation */
	void Start () {
		//The controller is what handles our movement in the game world
		controller = GetComponent<PlayerPhysics> ();
		audio = GetComponent<AudioSource> ();
		anim = GetComponent<Animation> ();
		animator = GetComponent<Animator> ();

		//Gravity setup
		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs (gravity) * timeToJumpApex;
		accelerationTimeAirborne = airborneAccelFast;
		anim.Stop ();

		//Delay and player statistics
		delay = 3;
		airCharge = 0;
		numberOfDeath = 0;

		//Collision checking for wall sliding speed
		collisionEnter = false;
		collisionContinuing = false;
	}

	/**
	 * Update is called every frame in order to update the player with the user's input and calculate the next movment to be handled by the PlayerPhysics class.
	 * Move() takes a Vector2 argument for the amount to be moved and carries it out on the player object. While an object is colliding with a surface, the controller.collisions field
	 * will provide information to the type of collision which can be used to test for jump validity, wall jumping etc.
	 */
	void Update () {

		//Handle cases where player cannot recieve input or move
		if (IsWaiting () || IsPaused ()) {
			return;
		}

		//Default to the idle animation
		//anim.Play ("Idle");

		//Checks if the player just collided with a wall to reset vertical sliding speed
		CheckCollisions ();

		// The current definition of a vertical wall is a platform with at least approx 75 degrees of elevation from horizontal.
		//Vertical collision detection. If the player touches the ground or ceiling set vertical velocity to zero.
		if (TouchingCeiling () || TouchingGround ()) {
			velocity.y = 0;
			//If player lands, reset airCharge
			if (TouchingGround ()) {
				airCharge = 1;
			}
		}

		//Get keyboard input
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		//Ignore left button if the object is on the right wall, and ignore right button if the object is on the left wall. 
		//Also, if the down button is pressed while the object is on a wall, it will slightly move the object off it 
		//and drop the object down. (With the raycasting code the current definition of a wall is at least approx 75 degrees
		//from horizontal). 
		if (TouchingWall () && !TouchingGround ()) {
			if (TouchingRightWall ()) {
				if (input.y == -1) {
					velocity.x = -moveSpeed / 100;
				}
				if (input.x == -1) {
					input.x = 0;
				}
			} else if (TouchingLeftWall ()) {
				if (input.y == -1) {
					velocity.x = moveSpeed / 100;
				}
				if (input.x == 1) {
					input.x = 0;
				}
			}
		}

		//When the jump button is pressed.
		if (Input.GetKeyDown (jumpKey)) { //Simply jump if the object is on the ground. 
			if (TouchingGround ()) {
				PlaySound (0);
				accelerationTimeAirborne = airborneAccelFast; //Update direction change speed
				velocity.y = jumpVelocity;
			}//If the object is touching a wall, jump in the opposite direction.
            else if (TouchingWall ()) {
				PlaySound (0);
				accelerationTimeAirborne = airborneAccelSlow; //Update direction change speed
				if (TouchingRightWall ()) {
					velocity.y = jumpVelocity;
					velocity.x = -moveSpeed * (float)1.5;
				} else if (TouchingLeftWall ()) {
					velocity.y = jumpVelocity;
					velocity.x = moveSpeed * (float)1.5;
				}
			} else if (airCharge == 1) {
				PlaySound (0);
				accelerationTimeAirborne = airborneAccelFast; //Update direction change speed
				velocity.y = jumpVelocity;
				velocity.x = moveSpeed * input.x;
				airCharge--;
			}
		}

		float targetVelocityX = input.x * moveSpeed;
		//Player accelerates towards top speed in the direction specified by input
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

		//Gliding sets a constant glidespeed
		if (Input.GetKey (glideKey) && airCharge == 0 && velocity.y < 0) {
			//Cannot be touching a wall to glide
			if (!TouchingWall () && !TouchingGround ()) {
				anim.Play ("Glide");
				velocity.y = glidingFallSpeed;
				targetVelocityX /= 2;

			}
		}
		//Gravity is applied
		else if (TouchingWall () && velocity.y < 2) {
			//Lower wall sliding speed if the player is falling faster than the sliding speed.
			if (collisionEnter && velocity.y < 0) {
				//Reduce verical velocity by a factor of 10
				velocity.y /= 10;
			}
			velocity.y += gravity * Time.deltaTime * slidingCoefficient;
		} else {
			velocity.y += gravity * Time.deltaTime;
		}

		//The controller is given a veloity to move the player by
		controller.Move (velocity * Time.deltaTime);
	}

	/**
	 * Check for side collisions on the first fram that they occur.
	 * collisionEnter is only true on the first fram of a side collision
	 */
	void CheckCollisions () {
		if (TouchingWall () && !collisionContinuing) {
			collisionEnter = true;
			collisionContinuing = true;
		} else if (TouchingWall () && collisionContinuing) {
			collisionEnter = false;
		} else if (!TouchingWall ()) {
			collisionEnter = false;
			collisionContinuing = false;
		}
	}

	//#################
	// Helper funcitons
	//#################

	//Return true if the player is waiting to move
	bool IsWaiting () {
		return Time.timeSinceLevelLoad < delay;
	}

	//Returns true if the game is paused
	bool IsPaused () {
		return Time.timeScale == 0f;
	}

	//Helper function to check if the object is touching the ground.
	bool TouchingGround () {
		return (controller.collisions.below);
	}

	//Helper function to check if the object is touching either the left or the right side walls.
	bool TouchingWall () {
		return (controller.collisions.left || controller.collisions.right);
	}

	//Helper function to check if the object is touching the right hand side wall.
	bool TouchingRightWall () {
		return (controller.collisions.right);
	}

	//Helper function to check if the object is touching the left hand side wall.
	bool TouchingLeftWall () {
		return (controller.collisions.left);
	}

	//Helper function to check if the object is touching the ceiling.
	bool TouchingCeiling () {
		return (controller.collisions.above);
	}

	void PlaySound (int clip) {
		audio.clip = audioClip [clip];
		audio.Play ();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Coin") {
			PlaySound (1);
		}
	}
}