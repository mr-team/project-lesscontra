using UnityEngine;
using System.Collections;

public class ShootingTrigger : MonoBehaviour
{
	public GameObject arrow;

	PlayerController playerController;

	void Start ()
	{
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> (); 
	}


	void Update ()
	{
	
	}

	void OnTriggerStay (Collider other)
	{
		Debug.Log ("somthing entered the trigger");

		if (other.tag == "Player")
		{
			playerController.SnapToPoint (transform.position);
			if (arrow != null)
				arrow.SetActive (true);

		}

	}
}
