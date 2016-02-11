using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class ArrowController : MonoBehaviour
{
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
	}


	void Update ()
	{
		if (arrowFired)
		{
			Velocity = arrowRigid.velocity;
			transform.forward = Vector3.Slerp (transform.forward, arrowRigid.velocity.normalized, Time.deltaTime * 2f); //point the arrow in the direction of the velocity
			ControlArrow ();
		}
	}

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

		Vector3 calcRotation = new Vector3 (startRotation.x * -rotationY, startRotation.y * rotationX, startRotation.z);

		Debug.Log ("velocity before modify: " + arrowRigid.velocity);

		arrowRigid.velocity = new Vector3 ((arrowRigid.velocity.x * calcRotation.x), arrowRigid.velocity.y, arrowRigid.velocity.z);

		Debug.Log ("velocity after modify: " + arrowRigid.velocity);
	}
}
	
