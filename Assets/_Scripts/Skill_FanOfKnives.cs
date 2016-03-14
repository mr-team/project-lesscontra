using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_FanOfKnives : MonoBehaviour
{
	Actor currActor;
	public Collider[] colliders;
	public List<Actor> Targets;
	float coolDown = 3f;
	float timer;
	float damage = 90f;
	float range = 3f;

	void Start ()
	{
		Targets = new List<Actor> ();
	}

	void Update ()
	{
		timer += Time.deltaTime;
		colliders = Physics.OverlapSphere (transform.position, range);

		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders [i].gameObject.GetComponent<Actor> () != null)
			{
				Targets.Add (colliders [i].GetComponent<Actor> ());	
			}
		}

		if (Input.GetKeyDown (KeyCode.Keypad1) && timer >= coolDown)
			ActivateFanOFKnives ();
		
		Targets.Clear ();
	}

	void ActivateFanOFKnives ()
	{
		timer = 0;

		Debug.Log ("I SHot my Load");
		for (int i = 0; i < Targets.Count; i++)
		{
			if (Targets [i].atributes.isHostile)
			{
				Targets [i].GiveDamage (damage);
			}
		}
	}
}
