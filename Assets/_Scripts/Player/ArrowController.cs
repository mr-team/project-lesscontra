using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class ArrowController : MonoBehaviour
{
	Transform standardRotate;
	Rigidbody arrowRigid;
	public bool arrowFired;
	float speed;
	float lerpSpeed;
	float force;

	void Awake ()
	{
		arrowRigid = gameObject.GetComponent<Rigidbody> ();
		arrowRigid.isKinematic = true;
		standardRotate = transform;
	}


	void Update ()
	{
		if (arrowFired)
			transform.forward = Vector3.Slerp (transform.forward, arrowRigid.velocity.normalized, Time.deltaTime * 2f);

	}

	public void FireArrow (float force)
	{
		arrowRigid.isKinematic = false;
		transform.SetParent (null);
		arrowRigid.AddRelativeForce (Vector3.forward * force);
		arrowFired = true;
	}

	void OnCollisionEnter (Collision other)
	{
		arrowRigid.isKinematic = true;

	}

	void ControlArrow ()
	{
		
	}
}
	
