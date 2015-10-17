using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class EnemyController : MonoBehaviour {
	//Movement variables
	float jumpHeight = 3f;
	float timeToJumpApex = .4f;
	float moveSpeed = 4;
	float dir = 1;

	float gravity;
	Vector3 velocity;
	public LevelManager levelManager;

	private bool canMove;
	private bool atEdge;
	private float delay;

	//The controller handles movement and collisions
	PlayerPhysics controller;
	
	/**Initialisation */
	void Start () {
		//The controller is what handles our movement in the game world
		controller = GetComponent<PlayerPhysics> ();

		//Level will handle all level based actions, such as player death
		levelManager = FindObjectOfType<LevelManager> ();

		//Variable setup
		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		canMove = false;
		atEdge = false;
		delay = 3;
	}

	/**
	 * Update is called every frame in order to update the player with the user's input and calculate the next movment to be handled by the PlayerPhysics class.
	 * Move() takes a Vector2 argument for the amount to be moved and carries it out on the player object. While an object is colliding with a surface, the controller.collisions field
	 * will provide information to the type of collision which can be used to test for jump validity, wall jumping etc.
	 */
	void Update () {
		//Delay for 3 seconds before the player can move.
		if (!canMove) {
			if (Time.timeSinceLevelLoad > delay) {
				canMove = true;	
			} else {
				//Do nothing
				return;
			}
		}

		// Checks if the game is paused or not
		if (!IsPaused ()) {
			//Vertical collision detection. If the player touches the ground or ceiling set vertical velocity to zero.
			if (TouchingGround () || TouchingCeiling ()) {
				velocity.y = 0;
			}

			//Initial movement
			if (velocity.x == 0) {
				velocity.x = moveSpeed * dir;
			}

			// Reverse direction if colliding with a wall
			if (TouchingWall ()) {
				velocity.x *= -1;
			}
            // Reverse direction if at edge of a platform
            else if (atEdge) {
				atEdge = false;
				velocity.x *= -1;
			}

			//Gravity is applied
			velocity.y += gravity * Time.deltaTime;

			//The controller is given a veloity to move the player by
			controller.Move (velocity * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D (Collider2D collision) {
		//When collision is detected with player, execute death of player
		if (collision.gameObject.tag == "Player") {
		//	Debug.Log ("Bang");
			levelManager.HandleDeath ();
		}
		//When collision is detected with edge invisible wall, determine atEdge to be true
		if (collision.gameObject.tag == "PatrolBoundary") {
		//	Debug.Log ("Invisible Wall");
			atEdge = true;
		}
	}

	//#################
	// Helper funcitons
	//#################

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

	bool IsPaused () {
		return (Time.timeScale == 0);
	}
}
