using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * NOTE: Uses basic ray-tracing template from https://github.com/SebLague/2DPlatformer-Tutorial/tree/master/Episode%2005
 */

[RequireComponent(typeof(PlayerPhysics))]
[RequireComponent(typeof(LevelManager))]
public class PlayerInput : MonoBehaviour {

	float jumpHeight = 3f;
	float timeToJumpApex = .4f;
	float accelerationTimeGrounded = .08f;
	float moveSpeed = 6;
	float slidingSpeed = 1.8f;
	float slidingCoefficient = 0.2f;
	float airborneAccelSlow = .35f;
	float airborneAccelFast = 0.13f;

	float gravity;
	float jumpVelocity;
	float velocityXSmoothing;
	float accelerationTimeAirborne;
	float airCharge;
	bool collisionEnter;
	bool collisionContinuing;
	Vector3 velocity;

	private float delay = 3; //Measured in seconds
	private int defaultJumpDelay = 5;

	private int jumpDelay = 20; //Measured in frames
	private DeathCount deathCount;
	public AudioClip[] audioClip;

	PlayerPhysics controller;
	AudioSource audio;

    //Replay recordings
    // public static List<Vector3> movementInputs = new List<Vector3>();
    //  public static List<Vector2> jumpInputs = new List<Vector2>();
    //  public static List<Vector3> movementReplayInputs = new List<Vector3>();
    //  public static List<Vector2> jumpReplayInputs = new List<Vector2>();

    public static List<Vector4> currentPosition = new List<Vector4>();
    public static List<Vector4> currentReplayPosition = new List<Vector4>();

    public int counter = 0; //counter for replay frame purposes.
    public string replay; //contains the replay string from the database (if any).

    public int debugCounter = 0;

    /**Initialisation */
    void Start () {
		//The controller is what handles our movement in the game world
		controller = GetComponent<PlayerPhysics> ();
		audio = GetComponent<AudioSource> ();

        

		//Gravity setup
		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs (gravity) * timeToJumpApex;
		accelerationTimeAirborne = airborneAccelFast;

		//Player ability setup
		airCharge = 0;




		//Collision checking for wall sliding speed
		collisionEnter = false;
		collisionContinuing = false;


        if(PlayerPrefs.GetInt("isReplay") == 1)
        {
            //movementReplayInputs = new List<Vector3>();
            //jumpReplayInputs = new List<Vector2>();
            currentReplayPosition = new List<Vector4>();
            ProcessReplay(PlayerPrefs.GetString("replayString"));
        }
        else
        {
            currentReplayPosition = new List<Vector4>();
            ProcessReplay(PlayerPrefs.GetString("replayString"));

            //movementInputs = new List<Vector3>();
            //jumpInputs = new List<Vector2>();
            //jumpReplayInputs = new List<Vector2>();
            //jumpReplayInputs.Add(new Vector2(0, 0));
        }

        counter = 0;
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

		//Checks if the player just collided with a wall to reset vertical sliding speed
		CheckCollisions ();

		//Count down for the jump delay
		if (TouchingGround()) {
			jumpDelay = defaultJumpDelay;
		}
		else if (jumpDelay != 0) {
			jumpDelay--;
		}


		// The current definition of a vertical wall is a platform with at least approx 75 degrees of elevation from horizontal.
		//Vertical collision detection. If the player touches the ground or ceiling set vertical velocity to zero.
		if (TouchingCeiling () || TouchingGround ()) {
			velocity.y = 0;
			//If player lands, reset airCharge
			if (TouchingGround ()) {
				airCharge = 1;
			}
		}




        Vector2 input;
        Vector4 replayInput = new Vector4();
        //Get keyboard input
      
        if(PlayerPrefs.GetInt("isReplay") == 1)//true if isReplay string is true i.e. it's a replay.
        {
            replayInput = GetInputFromReplay();
            //Debug.Log(counter);
            //Debug.Log(jumpReplayInputs[counter]);
            input.x = replayInput.y;
            input.y = replayInput.z;
        }
        else //if isReplay string is false i.e. it's not a replay.
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            RecordInput(input);
        }
		




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
		if ((Input.GetKeyDown (KeyCode.Space) && PlayerPrefs.GetInt("isReplay") == 0)) {
            Debug.Log("Jump is pressed");
            //Simply jump if the object is on the ground. 
			if (TouchingGround () || jumpDelay != 0) {
				if (TouchingGround()) {
					jumpDelay = 0;
				}
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
			}
			else if (airCharge == 1) {
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

		//Gravity is applied
		if (TouchingWall () && velocity.y < 2) {
			//Lower wall sliding speed if the player is falling faster than the sliding speed.
			if (collisionEnter && velocity.y < 0) {
				//Reduce verical velocity by a factor of 10
				velocity.y /= 10;
			}

			if (velocity.y > -slidingSpeed){
				velocity.y += gravity * Time.deltaTime * slidingCoefficient;
			} else {
				velocity.y = -slidingSpeed;
			}
		} else {
			velocity.y += gravity * Time.deltaTime;
		}

		//The controller is given a velocity to move the player by
        if(PlayerPrefs.GetInt("isReplay") == 0)
        {
          
            controller.Move(velocity * Time.deltaTime);
        }
		else
        {
            //  Debug.Log(currentReplayPosition[counter]);
            //Debug.Log("I am called");
            //Debug.Log(debugCounter);
            debugCounter++;
            controller.transform.position = new Vector3(currentReplayPosition[counter].y, currentReplayPosition[counter].z, currentReplayPosition[counter].w);
           // controller.transform.position = new Vector3(replayInput.x, replayInput.y, replayInput.z);

        }
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

    void RecordInput(Vector2 input)
    {
        float jumpPressed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = 1f;
        }
        else
        {
            jumpPressed = 0f;
        }
        
        currentPosition.Add(new Vector4(controller.transform.position.x, controller.transform.position.y, controller.transform.position.z, Time.timeSinceLevelLoad - delay));
        //Debug.Log(currentPosition[currentPosition.Count - 1]);
        //Debug.Log(currentPosition[currentPosition.Count - 1].w);
        //Debug.Log(currentPosition[currentPosition.Count - 1].x);
        //Debug.Log(currentPosition[currentPosition.Count - 1].y);
        //Debug.Log(currentPosition[currentPosition.Count - 1].z);
        
        //jumpInputs.Add(new Vector2(jumpPressed, Time.timeSinceLevelLoad - delay));
    }

    void ProcessReplay(string replayString)
    {
        //replay = ............;
        //replay = information from database.......

        //split the replay string into sections
        char[] delimiters = new char[] { ';', ','};
        string[] split = replayString.Split(new string[] { System.Environment.NewLine }, System.StringSplitOptions.None);
        //string[] split = replay.Split(new string[] { System.Environment.NewLine }, System.StringSplitOptions.None);

        string[] line1 = split[0].Split(delimiters);
        string[] line2 = split[1].Split(delimiters);

        for(int i = 0; i < line2.Length; i++)
        {
            //Debug.Log(line2[i]);
        }
        


        //convert the replay string sections into vector inputs to feed into the program.
        for (int i = 0; i < line2.Length - 3; i = i + 4)
        {
            currentReplayPosition.Add(new Vector4(float.Parse(line2[i + 3]), float.Parse(line2[i]), float.Parse(line2[i + 1]), float.Parse(line2[i+2])));
           // movementReplayInputs.Add(new Vector3(float.Parse(line2[i + 2]), float.Parse(line2[i]), float.Parse(line2[i + 1])));


        }
        //for (int i = 0; i < line3.Length - 1; i = i + 2)
        //{
          //  jumpReplayInputs.Add(new Vector2(float.Parse(line3[i + 1]), float.Parse(line3[i])));


        //}

        Debug.Log("Finished processing replay");
        //for(int i = 0; i < jumpInputs.Count; i++)
        //{
           //Debug.Log(jumpReplayInputs[i]);
        //}
       
    }

    //From a replay string, get the current input that should be fed into unity.
    Vector4 GetInputFromReplay()
    {

        for(int i = 0; i < currentReplayPosition.Count; i++)
        {
           if(currentReplayPosition[i].x > Time.timeSinceLevelLoad - delay)
            {
                counter = i;

                if (i == currentReplayPosition.Count - 1|| i == currentReplayPosition.Count - 2 || i == currentReplayPosition.Count - 3)
                {
                    Debug.Log("Last frames called");
                    currentReplayPosition[i] = new Vector4(currentReplayPosition[i].x, currentReplayPosition[i].y, currentReplayPosition[i].z - 0.2f, currentReplayPosition[i].w);
                    //currentReplayPosition[i].Set(currentReplayPosition[i].x, currentReplayPosition[i].y, currentReplayPosition[i].z - 0.2f, currentReplayPosition[i].w);

                }
                // Debug.Log(currentReplayPosition[counter]);
                return currentReplayPosition[counter];

             
            }

            
            

        }

        return currentReplayPosition[counter];
        // for (int i = counter; i < movementReplayInputs.Count; i++)
        //{
        //  if (movementReplayInputs[i].x > Time.timeSinceLevelLoad - delay && 
        //    movementReplayInputs[i].x < Time.timeSinceLevelLoad - delay + 0.2 )
        //{
        //  counter = i;

        //if(movementReplayInputs[i].x > 0.3 && i < movementReplayInputs.Count - 3)//after at least a couple of frames has passed.
        //{
        //  if(jumpReplayInputs[i].y == 0 && (jumpReplayInputs[i+1].y == 1 || jumpReplayInputs[i+2].y == 1 || jumpReplayInputs[i+3].y == 1))
        // {
        //   if(jumpReplayInputs[i+1].y == 1)
        // {
        //   counter = i + 1;
        //}
        //else if(jumpReplayInputs[i+2].y == 1)
        //{
        //  counter = i + 2;
        //}
        //else if(jumpReplayInputs[i+3].y == 1)
        //{
        //  counter = i + 3;
        //}

        //}

        //if(jumpReplayInputs[i].y == 1)
        //{
        //  counter = i + 1;
        //}

        //}

        //return movementReplayInputs[counter];

        //}
        //}
        //return movementReplayInputs[counter];
    }

    //#################
    // Helper functions
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