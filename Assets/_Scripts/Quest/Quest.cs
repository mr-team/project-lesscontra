using UnityEngine;
using System.Collections;

[System.Serializable]
public class Quest
{
	[SerializeField]
	protected Objective[] objectives;

	public Objective[] Objectives {
		get
		{
			return objectives;
		}
		set
		{
			objectives = value;
		}
	}

	Actor questGiver;
	bool active;
	bool complete;
	bool counted;

	int objComp;

	public bool Counted {
		get
		{
			return counted;
		}
		set
		{
			counted = value;
		}
	}

	public bool Complete {
		get
		{
			return complete;
		}
	}

	public bool Active {
		get
		{
			return active;
		}
		set
		{
			active = value;
		}
	}

	public Quest (Actor QusetGiver)
	{
		questGiver = questGiver;
	}

	public void Update ()
	{
		for (int i = 0; i < objectives.Length; i++)
		{
			if (!objectives [i].Compleate)
			{
				if (Objectives [i].KillTarget != null)
				{
					if (Objectives [i].KillTarget.isDead)
						objectives [i].Compleate = true;
				}
				if (Objectives [i].DestroyTarget != null)
				{
					
				}
				if (Objectives [i].DialogueTarget != null)
				{

				}
			} else if (objectives [i].Compleate)
			{
				objComp++;
			}
		}

		if (objComp == objectives.Length)
		{
			
			complete = true;
			active = false;
		}
	}
}