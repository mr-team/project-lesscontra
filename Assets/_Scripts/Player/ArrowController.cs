using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour
{
	public Transform rotateUp;
	public Transform rotateDown;
	public Transform rotateRight;
	public Transform rotateLeft;

	Transform standardRotate;
	Rigidbody arrowRigid;
	bool arrowFired;
	public float speed;
	public float lerpSpeed;

	void Start ()
	{
		gameObject.SetActive (false);
		arrowRigid = gameObject.GetComponent<Rigidbody> ();
		standardRotate = transform;
	}


	void Update ()
	{
		if (Input.GetMouseButtonDown (0) && !arrowFired)
		{
			arrowFired = true;
		}
		if (arrowFired)
		{
			FireArrow ();
			ControlArrow ();
		}
	}



	

	void FireArrow ()
	{
		//transform.Translate (Vector3.forward * speed);
		transform.localPosition += transform.forward * Time.deltaTime * speed;
	}

	void ControlArrow ()
	{
		if (Input.GetKey (KeyCode.W))
		{
			transform.rotation = Quaternion.Lerp (standardRotate.rotation, rotateUp.rotation, lerpSpeed);

		}
		if (Input.GetKey (KeyCode.A))
		{
			transform.rotation = Quaternion.Lerp (standardRotate.rotation, rotateLeft.rotation, lerpSpeed);
		}

		if (Input.GetKey (KeyCode.S))
		{
			transform.rotation = Quaternion.Lerp (standardRotate.rotation, rotateDown.rotation, lerpSpeed);
		}

		if (Input.GetKey (KeyCode.D))
		{
			transform.rotation = Quaternion.Lerp (standardRotate.rotation, rotateRight.rotation, lerpSpeed);
		}



	}
}
	
