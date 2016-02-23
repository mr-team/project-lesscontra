using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof(Player))]
[RequireComponent (typeof(NavMeshAgent))]

[Serializable]
public class PlayerControllerReferances
{
	public GameObject TPMode;
	public GameObject FPMode;
	public GameObject Arrow;
	public GameController gameControll;
}

public class PlayerController : MonoBehaviour
{
	public GameObject arrowPrefab;
	public PlayerControllerReferances referances;
	public bool shouldCameraFollowPlayer = true;

	//Referances
	ArrowController arrowControll;
	CameraController FPCamControll;
	UIFeedBack UIfeedBack;
	Player player;
	Animator playerAnim;
	NavMeshAgent navAgent;

	//clickToMove
	[HideInInspector]
	public Vector3 targetPosition;
	Vector3 lastTargetPos;

	bool isCrouching;
	bool isProne;
	bool isWalking;
	bool isRunning;
	bool isJumping;

	float moveSpeed;
	float distFromWall;
	float timer;

	//FPMode
	[HideInInspector]
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
		arrowControll = referances.Arrow.GetComponent<ArrowController> ();
		moveSpeed = player.stats.walkSpeed;
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
			Camera.main.transform.position = new Vector3 (this.transform.position.x, 30f, this.transform.position.z);
		
		ChangeState (); 	//Update the movement state of the player

		if (Input.GetMouseButton (0) && !FPModeActive)
		{
			if (CheckClickedLayer () == 8)  	//if the clicked layer was the ground...
			{
				SetTargetPosition (CheckClickedLayer ()); 	//...set the target position to the clicket point, and...
				ClickToMove ();		//..move the player to the clicked point

			} else if (CheckClickedLayer () == 10) 		//if the clicked layer was a scalable wall
			{
				SetTargetPosition (CheckClickedLayer (), GetClickedTransform ()); //move the player to the "jump" spot by the wall
			}
		}

		if (Input.GetMouseButtonDown (0) && !FPModeActive)
		{
			if (CheckClickedLayer () == 8)
			{
				lastTargetPos = targetPosition;
				referances.gameControll.CallUI_WalkPoint (targetPosition);

			}
		}

		if (Input.GetMouseButtonUp (0) && !FPModeActive && targetPosition != lastTargetPos)
		{
			if (CheckClickedLayer () == 8)
			{	
				
				referances.gameControll.CallUI_WalkPoint (targetPosition);
			}
		}
			
		if (Input.GetKeyDown (KeyCode.Space))
		{
			if (FPModeActive)
			{
				GoToTPMode ();
				return;
			}
				
			if (!FPModeActive)
			{
				GoToFPMode ();
			}
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
				
				GameObject tempArrow = Instantiate (arrowPrefab, arrowControll.transform.position, FPCamControll.FPModeAnchor.rotation)as GameObject;
				tempArrow.GetComponent<ArrowController> ().FireArrow ();

				fireForce = 0F;
			}
		}

		HandleAnimation (); //handle animation transitions

        Transform hit = GetClickedTransform();
        if(hit != null && hit.CompareTag("Npc_Guard")) {
            GetComponent<MeleeAttack>().addTarget(hit.GetComponent<NPC_Generic>());
        }
    }

	public void ClickToMove ()
	{
		if (isRunning)
		{
			moveSpeed = player.stats.runSpeed;
		}
		if (isWalking)
		{
			moveSpeed = player.stats.walkSpeed;
		}
		if (isCrouching)
		{
			moveSpeed = player.stats.crouchSpeed;
		}
		if (isProne)
		{
			moveSpeed = player.stats.proneSpeed;
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
		referances.FPMode.SetActive (true);
		referances.TPMode.SetActive (false);
		referances.Arrow.SetActive (false);

		Cursor.visible = (false);
		Cursor.lockState = CursorLockMode.Locked;
		arrowControll.gameObject.SetActive (true);

		FPModeActive = true;
		arrowModeActive = false;


	}

	public void GoToTPMode ()
	{
		referances.TPMode.SetActive (true);
		referances.FPMode.SetActive (false);
		referances.Arrow.SetActive (false);

		Cursor.visible = (true);
		Cursor.lockState = CursorLockMode.None;

		FPModeActive = false;
		arrowModeActive = false;
	}

	public void GoToArrowMode ()
	{
		referances.Arrow.SetActive (true);
		referances.TPMode.SetActive (false);
		referances.FPMode.SetActive (false);

		Cursor.visible = (true);
		Cursor.lockState = CursorLockMode.None;

		FPModeActive = false;
		arrowModeActive = true;

	}
}
