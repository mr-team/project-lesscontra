using UnityEngine;
using System.Collections;

public class DropObject : MonoBehaviour
{
	public GameObject restrainer;

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Weapon")	//if the trigger detects a weapon...
			Drop ();	//...drop the object.
	}

	void Drop ()
	{
		Destroy (restrainer);	//destroy the restrainer
		gameObject.GetComponent<Rigidbody> ().isKinematic = false;	//set the object to notkinematic
	}
}