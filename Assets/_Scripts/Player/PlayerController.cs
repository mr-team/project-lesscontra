using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Player))]
[RequireComponent (typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
	public bool shouldCameraFollowPlayer = true;
	public bool MoveWithArrows;

	Player player;
	Animator playerAnim;

	//For the click to move function
	NavMeshAgent navAgent;

	Vector3 targetPosition;

	public bool isCrouching;
	public bool isProne;
	public bool isWalking;
	public bool isRunning;
	public bool isJumping;

	float moveSpeed;

	public float distFromWall;

	float timer;

	void Awake ()
	{
		player = GetComponent<Player> ();
		navAgent = GetComponent<NavMeshAgent> ();
		playerAnim = GetComponent<Animator> ();
		moveSpeed = player.walkSpeed;
	}

	void Start ()
	{
		targetPosition = transform.position;

	}

	void Update ()
	{
		if (shouldCameraFollowPlayer)
			Camera.main.transform.position = new Vector3 (this.transform.position.x - 15f, 30f, this.transform.position.z - 15f);
		
		//movement
		if (MoveWithArrows) 	//should the player be controlled by WASD... 
			WASDMove ();
		else if (!MoveWithArrows)	//... or by clicking on the ground
		{
			ChangeState (); 	//Update the movement state of the player

			if (Input.GetMouseButton (0))
			{
				if (CheckClickedLayer () == 8)  	//if the clicked layer was the ground
				{
					SetTargetPosition (CheckClickedLayer ()); 	//set the target position to the clicket point
					ClickToMove ();		//move the player to the clicked point

				} else if (CheckClickedLayer () == 10) 		//if the clicked layer was a scalable wall
				{
					SetTargetPosition (CheckClickedLayer (), GetClickedTransform ()); //move the player to the "jump" spot by the wall
				}
			}
		}

		HandleAnimation (); //handle animation transitions
	}

	void WASDMove ()
	{
		float moveX = Input.GetAxis ("Horizontal");
		float movez = Input.GetAxis ("Vertical");
		float Speed = player.walkSpeed;
		float crouchSpeed = player.walkSpeed * 0.6f;

		Vector3 dir = new Vector3 (moveX, 0, movez);

		if (Input.GetKey (KeyCode.LeftShift))
		{
			Speed = crouchSpeed;
		}
		transform.localPosition += dir * Speed * Time.deltaTime;
	}

	void ClickToMove ()
	{
		if (isRunning)
		{
			moveSpeed = player.runSpeed;
		}
		if (isWalking)
		{
			moveSpeed = player.walkSpeed;
		}
		if (isCrouching)
		{
			moveSpeed = player.crouchSpeed;
		}
		if (isProne)
		{
			moveSpeed = player.proneSpeed;
		}
		 
		Debug.Log (targetPosition);

		navAgent.speed = moveSpeed;
		navAgent.SetDestination (targetPosition);
	}

	/// <summary>
	/// get the position of the mouse click.
	/// </summary>
	void SetTargetPosition (int layer)
	{
		if (layer == 8)
		{
			Plane plane = new Plane (Vector3.up, transform.position);
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float point = 0f;

			if (plane.Raycast (ray, out point))
				targetPosition = ray.GetPoint (point);	
		}
	}

	/// <summary>
	/// get the position of the mouse click.
	/// </summary>
	void SetTargetPosition (int layer, Transform clickedTransform)
	{
		if (layer == 8)
		{
			Plane plane = new Plane (Vector3.up, transform.position);
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float point = 0f;

			if (plane.Raycast (ray, out point))
				targetPosition = ray.GetPoint (point);	
		}
		if (layer == 10)
		{
			MoveToWall (clickedTransform);
		}
	}

	/// <summary>
	/// /Changes the movement state of the player*/
	/// </summary>
	void ChangeState ()
	{
		if (Input.GetButtonDown ("Crouch"))
		{
			isCrouching = true;

			isProne = false;
			isWalking = false;
			isRunning = false;
		}

		if (Input.GetButtonDown ("Prone"))
		{
			isProne = true;

			isCrouching = false;
			isWalking = false;
			isRunning = false;
		}

		if (Input.GetButtonDown ("Walk"))
		{
			isWalking = true;

			isProne = false;
			isCrouching = false;
			isRunning = false;
		}

		if (Input.GetButtonDown ("Run"))
		{
			isRunning = true;

			isProne = false;
			isCrouching = false;
			isWalking = false;
		}	
	}

	public void SetState (string state)
	{
		if (state == "isJumping")
		{
			isJumping = true;

			isProne = false;
			isCrouching = false;
			isWalking = false;
			isRunning = false;
		}
	}

	void HandleAnimation ()
	{
		if (isJumping)
		{
			playerAnim.SetBool ("isJumping", true);
			playerAnim.applyRootMotion = false;

			timer += Time.deltaTime;

			if (timer >= playerAnim.GetCurrentAnimatorStateInfo (0).length)
			{
				playerAnim.SetBool ("isJumping", false);

			}
		}
	}

	int CheckClickedLayer ()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit;

		if (Physics.Raycast (ray, out hit) && hit.transform != null)
			return hit.transform.gameObject.layer;

		return 9;
	}

	Transform GetClickedTransform ()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit;

		if (Physics.Raycast (ray, out hit) && hit.transform != null)
			return hit.transform;

		return null;
	}

	/// <summary>
	/// makes the player jump the clicked wall. 
	/// If the player is running he will jump differently as oposed to if the player was walking
	/// </summary>
	void MoveToWall (Transform clickedWall)
	{
		Vector3 jumpPoint = new Vector3 (clickedWall.position.x + distFromWall, 1.2f, clickedWall.position.z);
		//Set destination to wall

		navAgent.SetDestination (jumpPoint);

	}

	public void JumpWall ()
	{
		
	}

	public void SnapToPoint (Vector3 point)
	{
		if (point == transform.position)
			return;

		transform.position = point;
	}
}
