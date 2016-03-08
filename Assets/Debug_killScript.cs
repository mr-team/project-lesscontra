using UnityEngine;
using System.Collections;

public class Debug_killScript : MonoBehaviour
{

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Y))
			this.gameObject.GetComponent<NPC_Generic> ().isDead = true;
	}
}
