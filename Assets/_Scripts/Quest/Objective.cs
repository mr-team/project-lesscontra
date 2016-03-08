using UnityEngine;
using System.Collections;

[System.Serializable]
public class Objective
{
	
	Actor killTarget;
	Actor dialogueTarget;
	GameObject destroyTarget;

	public Actor KillTarget {
		get
		{
			return killTarget;
		}
	}

	public Actor DialogueTarget {
		get
		{
			return dialogueTarget;
		}
	}

	public GameObject DestroyTarget {
		get
		{
			return destroyTarget;
		}
	}

	bool compleate;

	public bool Compleate {
		get
		{
			return compleate;
		}
		set
		{
			compleate = value;
		}
	}

	public Objective (Actor KillTargets = null, Actor DialogueTarget = null, GameObject DestroyTarget = null)
	{
		killTarget = KillTargets;
		dialogueTarget = DialogueTarget;
		destroyTarget = DestroyTarget;
	}
}
