using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	static Player player;

	public float runSpeed = 10;
	public float walkSpeed = 10;
	public float crouchSpeed = 10;
	public float proneSpeed = 10;

	void Start ()
	{
		if (player == null)
			player = GetComponent<Player> ();
	}


	void Update ()
	{
	
	}
}
