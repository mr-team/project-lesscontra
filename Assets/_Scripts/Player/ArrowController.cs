using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class ArrowController : MonoBehaviour
{
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
	}

	void Update ()
	{
		
		if (arrowFired)
		{
			ControlArrow ();
		}
	}

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
	}

	public void FireArrow (float force, float angle)
	{
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
		}
	}

	void OnCollisionEnter (Collision other)
	{
		arrowRigid.isKinematic = true; //when the arrow hits somthing, make it freeze in the air. Temp solution.
		arrowFired = false;
		hitSomthing = true;
		playerControll.GoToTPMode ();

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
	}
}
	
