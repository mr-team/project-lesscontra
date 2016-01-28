using UnityEngine;
using System.Collections;

public class ScalableWall : MonoBehaviour
{
	PlayerController playerControll;

	void Start ()
	{
		playerControll = GameObject.Find ("Player").GetComponent<PlayerController> ();
	}

	void Update ()
	{
	
	}

	void OnTriggerStay (Collider other)
	{
		if (other.tag == "Player")
		{
			playerControll.SetState ("isJumping");

			playerControll.JumpWall ();
		}		
	}
}
