using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class ArrowController : MonoBehaviour
{

	public CameraController camControll;

	Arrow arrow;
	Rigidbody arrowRigid;
	PlayerController playerControll;

	public bool arrowFired;

	bool hitSomthing;

	float speed = 10f;

	public float sensitivityX;
	public float sensitivityY;
	public float minimumX;
	public float maximumX;
	public float minimumY;
	public float maximumY;
	float rotationX;
	float rotationY;

	bool physicsMode;
	bool reset;

	void Awake ()
	{
		arrowRigid = gameObject.GetComponent<Rigidbody> (); //find the arrows rigidbody
		arrow = GetComponent<Arrow> ();
		playerControll = GameObject.Find ("Player").GetComponent<PlayerController> ();

	}

	void Update ()
	{

		ControlArrow ();

		
	}


	void FixedUpdate ()
	{
		/*if (arrowFired && physicsMode)
		{
			transform.Translate (Vector3.forward.x * speed * Time.deltaTime, 0, Vector3.forward.z * speed * Time.deltaTime);
			ControlArrowXZ ();
		}*/

		if (arrowFired && !physicsMode && !hitSomthing)
		{
			//transform.localPosition = transform.forward * Time.deltaTime * speed / 2;
		}

	}

	public void FireArrow ()
	{
		playerControll.GoToArrowMode ();
		transform.SetParent (null); //makes the arrow independent of the Player
		arrowFired = true;

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

		rotationY += transform.localEulerAngles.z + Input.GetAxis ("Mouse Y") * sensitivityY;
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

		Vector3 calcRotation = new Vector3 (-rotationY, rotationX, 0);

		transform.localEulerAngles = calcRotation;

	}
}
	
