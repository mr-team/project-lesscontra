using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Player))]
[RequireComponent (typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
	public bool shouldCameraFollowPlayer = true;
	public GameObject TPMode;
	public GameObject FPMode;
	public GameObject Arrow;

	ArrowController arrowControll;
	CameraController FPCamControll;

	Player player;
	Animator playerAnim;

	//ClickToMove
	NavMeshAgent navAgent;

	public Vector3 targetPosition;

	bool isCrouching;
	bool isProne;
	bool isWalking;
	bool isRunning;
	bool isJumping;

	float moveSpeed;
	float distFromWall;
	float timer;

	//FPMode
	public bool FPModeActive;
	bool transitionToFPMode;
	bool hasFiredArrow;

	float minForce;
	float maxForce;
	float fireForce;

	//ArrowMode
	bool arrowModeActive;

	void Awake ()
	{
		player = GetComponent<Player> ();
		navAgent = GetComponent<NavMeshAgent> ();
		playerAnim = GetComponent<Animator> ();
		FPCamControll = GetComponent<CameraController> ();
		arrowControll = Arrow.GetComponent<ArrowController> ();

		moveSpeed = player.walkSpeed;
		GoToTPMode ();
		FPModeActive = false;
	}

	void Start ()
	{
		targetPosition = transform.position;
	}

	void Update ()
	{
		if (shouldCameraFollowPlayer && !FPModeActive)
			Camera.main.transform.position = new Vector3 (this.transform.position.x - 15f, 30f, this.transform.position.z - 15f);
		
		ChangeState (); 	//Update the movement state of the player

		if (Input.GetMouseButton (0) && !FPModeActive)
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

		if (Input.GetKeyDown (KeyCode.J) && FPModeActive)
		{
			GoToTPMode ();

		}
			
		if (Input.GetKeyDown (KeyCode.K) && !FPModeActive)
		{
			GoToFPMode ();

		}

		if (FPModeActive)
		{
			if (Input.GetMouseButton (0))
			{
				float modifyer = 300f;

				fireForce += Time.deltaTime * modifyer;

			}
			if (Input.GetMouseButtonUp (0))
			{
				if (fireForce < minForce)
					fireForce = minForce;
				if (fireForce > maxForce)
					fireForce = maxForce;
				
				//arrowControll.FireArrow (fireForce, Mathf.Abs (FPCamControll.planarRotationX.x - 360f));
				arrowControll.FireArrow ();
				fireForce = 0F;
			}
		}

		HandleAnimation (); //handle animation transitions
	}

	public void ClickToMove ()
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
			/*Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			Plane plane = new Plane (Vector3.up, mousePos); //creates a flat plane with a normal up, at the point of the player STUPID!

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); //creates a ray from camerea throug mouse pointer
			float point = 0f;

			if (plane.Raycast (ray, out point)) //is the ray intercepts the plane return its point 
				targetPosition = ray.GetPoint (point);	//store the point in a vector 3*/

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) //if the raycast hit somthing
			{
				if (hit.transform.gameObject.layer == 8) //if it hit an object in the ground layer
					targetPosition = hit.point; //get the point where ray hit the object
			}
		}
	}

	/// <summary>
	/// get the position of the mouse click.
	/// </summary>
	void SetTargetPosition (int layer, Transform clickedTransform)
	{
		if (layer == 8)
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit))
			{
				if (hit.transform.gameObject.layer == 8)
					targetPosition = hit.point;
			}
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

	public void GoToFPMode ()
	{
		FPMode.SetActive (true);
		TPMode.SetActive (false);
		Arrow.SetActive (false);

		Cursor.visible = (false);
		Cursor.lockState = CursorLockMode.Locked;
		arrowControll.gameObject.SetActive (true);

		FPModeActive = true;
		arrowModeActive = false;
	}

	public void GoToTPMode ()
	{
		TPMode.SetActive (true);
		FPMode.SetActive (false);
		Arrow.SetActive (false);

		Cursor.visible = (true);
		Cursor.lockState = CursorLockMode.None;

		FPModeActive = false;
		arrowModeActive = false;
	}

	public void GoToArrowMode ()
	{
		Arrow.SetActive (true);
		TPMode.SetActive (false);
		FPMode.SetActive (false);

		Cursor.visible = (true);
		Cursor.lockState = CursorLockMode.None;

		FPModeActive = false;
		arrowModeActive = true;
	}
}
