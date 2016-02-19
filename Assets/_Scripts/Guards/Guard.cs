using UnityEngine;
using System.Collections;

[RequireComponent (typeof(FieldOfView))]
[RequireComponent (typeof(Rigidbody))]
//[RequireComponent (typeof(CanIWalkNow))]
public class Guard : Actor, IKillable
{


	void HandleAnimation ()
	{
		
	}

	public void Kill ()
	{
		if (stats.health <= 0)
			Destroy (gameObject);
	}

}

