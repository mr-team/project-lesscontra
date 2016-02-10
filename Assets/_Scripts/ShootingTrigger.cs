using UnityEngine;
using System.Collections;

public class ShootingTrigger : MonoBehaviour
{

	PlayerController playerController;

	void Start ()
	{
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> (); 
	}


	void Update ()
	{
	
	}

	void OnTriggerEnter (Collider other)
	{
		

		if (other.tag == "Player")
		{
			playerController.SnapToPoint (transform.position);
			playerController.GoToFPMode ();

		}

	}
}
