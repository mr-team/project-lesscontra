using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	static Player player;

	public float runSpeed;
	public float walkSpeed;
	public float crouchSpeed;
	public float proneSpeed;

	void Start ()
	{
		if (player == null)
			player = GetComponent<Player> ();
	}
}
