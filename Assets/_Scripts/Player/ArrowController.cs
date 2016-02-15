using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class ArrowController : MonoBehaviour
{
	public Transform arrowAnchor;
	public CameraController camControll;
	Transform standardRotate;
	Rigidbody arrowRigid;

	public bool arrowFired;

	Vector3 startRotation;
	Vector3 calcRotation;
	Vector3 Velocity;
	Vector3 newVelocityDir;

	Vector3 CurrentAngle;

	public Vector3 MaxRotateLeft;
	public Vector3 MaxRotateRight;
	public Vector3 MaxRotateUp;
	public Vector3 MaxRotateDown;

	float speed = 10f;
	float lerpSpeed;


	public float sensitivityX;
	public float sensitivityY;
	public float minimumX;
	public float maximumX;
	public float minimumY;
	public float maximumY;
	float rotationX;
	float rotationY;

	public bool physicsMode;

	void Awake ()
	{
		arrowRigid = gameObject.GetComponent<Rigidbody> (); //find the arrows rigidbody
		arrowRigid.isKinematic = true; //makes sure the arrow does not fall to the ground before it is fired
	}

	void Update ()
	{
		transform.Rotate (new Vector3 (0, 0, 0));
		if (!arrowFired)
		{

			transform.position = arrowAnchor.position;
			transform.localEulerAngles = (camControll.planarRotationY);
		}
		if (arrowFired)
		{
			ControlArrow ();
		}
	}


	void FixedUpdate ()
	{
		//CurrentAngle = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

		if (arrowFired && physicsMode)
		{
			transform.Translate (Vector3.forward.x * speed * Time.deltaTime, 0, Vector3.forward.z * speed * Time.deltaTime);
		
			ControlArrowXZ ();
		
		}

		if (arrowFired && !physicsMode)
		{
			transform.localPosition += transform.forward * Time.deltaTime * speed;
		}
	}

	public void FireArrow (float force, float angle)
	{
		if (!arrowFired && physicsMode)
		{
			
			//physics based
			float forceY = Mathf.Abs ((force * 2) * Mathf.Sin (angle * Mathf.Deg2Rad));

			Debug.Log ("force passed: " + force);
			Debug.Log ("sin: " + Mathf.Sin (angle * Mathf.Deg2Rad));
			Debug.Log ("y comp of force: " + forceY);

			arrowRigid.isKinematic = false;
			transform.SetParent (null); //makes the arrow independent of the camera movement
			arrowRigid.AddForce (Vector3.up * forceY); //fires the arrow straight up


			//non physics based
			float forceX = Mathf.Abs (force * Mathf.Cos (angle * Mathf.Deg2Rad));
			speed = forceX * 0.06f;
			Debug.Log ("Speed: " + speed);
		
			arrowFired = true;
		}

		if (!arrowFired && !physicsMode)
		{
			arrowFired = true;
		}

	}

	void OnCollisionEnter (Collision other)
	{
		arrowRigid.isKinematic = true; //when the arrow hits somthing, make it freeze in the air. Temp solution.
		arrowFired = false;
	}

	void ControlArrowXZ ()
	{
		if (Input.GetKey (KeyCode.A))
		{
			CurrentAngle = new Vector3 (
				CurrentAngle.x,
				Mathf.LerpAngle (CurrentAngle.y, MaxRotateRight.y, Time.deltaTime),
				CurrentAngle.z);
		
			transform.eulerAngles = CurrentAngle;
		}
		if (Input.GetKey (KeyCode.D))
		{
			CurrentAngle = new Vector3 (
				CurrentAngle.x,
				Mathf.LerpAngle (CurrentAngle.y, MaxRotateLeft.y, Time.deltaTime),
				CurrentAngle.z);

			transform.eulerAngles = CurrentAngle;
		}
	}

	void ControlArrow ()
	{
		if (Input.GetKey (KeyCode.A))
		{
			CurrentAngle = new Vector3 (
				CurrentAngle.x,
				Mathf.LerpAngle (CurrentAngle.y, MaxRotateRight.y, Time.deltaTime * 2),
				CurrentAngle.z);

			transform.eulerAngles = CurrentAngle;
		}
		if (Input.GetKey (KeyCode.D))
		{
			CurrentAngle = new Vector3 (
				CurrentAngle.x,
				Mathf.LerpAngle (CurrentAngle.y, MaxRotateLeft.y, Time.deltaTime * 2),
				CurrentAngle.z);

			transform.eulerAngles = CurrentAngle;
		}
		if (Input.GetKey (KeyCode.W))
		{
			CurrentAngle = new Vector3 (
				Mathf.LerpAngle (CurrentAngle.x, MaxRotateUp.x, Time.deltaTime * 2),
				CurrentAngle.y,
				CurrentAngle.z);

			transform.eulerAngles = CurrentAngle;
		}
		if (Input.GetKey (KeyCode.S))
		{
			CurrentAngle = new Vector3 (
				Mathf.LerpAngle (CurrentAngle.x, MaxRotateDown.x, Time.deltaTime * 2),
				CurrentAngle.y,
				CurrentAngle.z);

			transform.eulerAngles = CurrentAngle;
		}
	}


}
	
