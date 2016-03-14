using UnityEngine;
using System.Collections;

public class NPC_Guard : Actor
{

	void Start ()
	{
	
	}

	protected override void Update ()
	{
		base.Update ();
	}

	public override void Kill ()
	{
		base.Kill ();
		isDead = true;
		Destroy (gameObject);
	}
}
