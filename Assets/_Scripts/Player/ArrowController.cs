using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class ArrowController : MonoBehaviour
{
<<<<<<< HEAD
	public CameraController camControll;
	Arrow arrow;
	Transform standardRotate;
	Rigidbody arrowRigid;
	PlayerController playerControll;
	public bool arrowFired;

	bool hitSomthing;

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
		arrow = GetComponent<Arrow> ();
		playerControll = GameObject.Find ("Player").GetComponent<PlayerController> ();
=======
	Transform standardRotate;
	Rigidbody arrowRigid;

	public bool arrowFired;

	Vector3 startRotation;
	Vector3 calcRotation;
	Vector3 Velocity;
	Vector3 newVelocityDir;

	float speed;
	float lerpSpeed;
	float force;

	public float sensitivityX;
	public float sensitivityY;
	public float minimumX;
	public float maximumX;
	public float minimumY;
	public float maximumY;
	float rotationX;
	float rotationY;

	void Awake ()
	{
		arrowRigid = gameObject.GetComponent<Rigidbody> (); //find the arrows rigidbody
		arrowRigid.isKinematic = true; //makes sure the arrow does not fall to the ground before it is fired
>>>>>>> refs/remotes/mr-team/master
	}

	void Update ()
	{
<<<<<<< HEAD
		
		if (arrowFired)
		{
=======
		if (arrowFired)
		{
			Velocity = arrowRigid.velocity;
			transform.forward = Vector3.Slerp (transform.forward, arrowRigid.velocity.normalized, Time.deltaTime * 2f); //point the arrow in the direction of the velocity
>>>>>>> refs/remotes/mr-team/master
			ControlArrow ();
		}
	}

<<<<<<< HEAD
	void FixedUpdate ()
	{
		if (arrowFired && physicsMode)
		{
			transform.Translate (Vector3.forward.x * speed * Time.deltaTime, 0, Vector3.forward.z * speed * Time.deltaTime);
			ControlArrowXZ ();
		}

		if (arrowFired && !physicsMode && !hitSomthing)
		{
			transform.localPosition += transform.forward * Time.deltaTime * speed / 2;
		}
=======
	public void FireArrow (float force)
	{
		arrowRigid.isKinematic = false; //the arrow is now handeled by the physics engine
		transform.SetParent (null); //makes the arrow independent of the camera movement
		arrowRigid.AddRelativeForce (Vector3.forward * force); //fires the arrow in the arrows relative forward direction
		arrowFired = true;
		startRotation = transform.localEulerAngles;
		Debug.Log ("the startRotation is: " + startRotation);
	}

	void OnCollisionEnter (Collision other)
	{
		arrowRigid.isKinematic = true; //when the arrow hits somthing, make it freeze in the air. Temp solution.

>>>>>>> refs/remotes/mr-team/master
	}

	public void FireArrow (float force, float angle)
	{
<<<<<<< HEAD
		if (!arrowFired && physicsMode)
		{
			//physics based
			float forceY = Mathf.Abs ((force * 2) * Mathf.Sin (angle * Mathf.Deg2Rad));

			transform.SetParent (null); //makes the arrow independent of the Player
			arrowRigid.AddForce (Vector3.up * forceY); //fires the arrow straight up

			//non physics based
			float forceX = Mathf.Abs (force * Mathf.Cos (angle * Mathf.Deg2Rad));
			speed = forceX * 0.06f;
			Debug.Log ("Speed: " + speed);
		
			arrowFired = true;
		}

		if (!arrowFired && !physicsMode)
		{
			playerControll.GoToArrowMode ();
			transform.SetParent (null); //makes the arrow independent of the Player
			arrowFired = true;
=======
		rotationX = transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityX;

		if (rotationX > 180)
		{
			rotationX -= 360;
>>>>>>> refs/remotes/mr-team/master
		}
	}

	void OnCollisionEnter (Collision other)
	{
		arrowRigid.isKinematic = true; //when the arrow hits somthing, make it freeze in the air. Temp solution.
		arrowFired = false;
		hitSomthing = true;
		playerControll.GoToTPMode ();

<<<<<<< HEAD
		if (other.transform.tag == "Guard")
		{
			//transfer damage to guard
		}
	}

	void ControlArrowXZ ()
	{
		
	}

	void ControlArrow ()
	{
		rotationX = transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityX;

		if (rotationX > 180)
		{
			rotationX -= 360;
		}

		if (rotationX <= minimumX)
			rotationX = minimumX;

		if (rotationX >= maximumX)
			rotationX = maximumX;

		rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

		Vector3 calcRotation = new Vector3 (-rotationY, rotationX, 0);

		transform.localEulerAngles = calcRotation;
=======
		if (rotationX <= minimumX)
			rotationX = minimumX;

		if (rotationX >= maximumX)
			rotationX = maximumX;

		Vector3 calcRotation = new Vector3 (startRotation.x * -rotationY, startRotation.y * rotationX, startRotation.z);

		Debug.Log ("velocity before modify: " + arrowRigid.velocity);

		arrowRigid.velocity = new Vector3 ((arrowRigid.velocity.x * calcRotation.x), arrowRigid.velocity.y, arrowRigid.velocity.z);

		Debug.Log ("velocity after modify: " + arrowRigid.velocity);
>>>>>>> refs/remotes/mr-team/master
	}
}
	
