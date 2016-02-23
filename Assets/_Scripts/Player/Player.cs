using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PlayerStats
{
	public float runSpeed;
	public float walkSpeed;
	public float crouchSpeed;
	public float proneSpeed;
}

public class Player : MonoBehaviour
{
	public PlayerStats stats;
	static Player player;

	void Start ()
	{
		if (player == null)
			player = GetComponent<Player> ();
	}
}
