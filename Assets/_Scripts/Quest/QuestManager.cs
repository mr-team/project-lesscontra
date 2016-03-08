using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class QuestManager : MonoBehaviour
{
	public Quest[] activeQuests = new Quest[2];
	public Quest[] completedQuests = new Quest[2];

	[SerializeField]
	public List<Quest> quests;

	public List<Quest> Quests {
		get
		{
			return quests;
		}
		set
		{
			quests = value;
		}
	}

	int totalQuests = 1;
	int questsComplete;

	void Update ()
	{
		for (int i = 0; i < activeQuests.Length; i++)
		{
			if (activeQuests [i] != null)
			{
				if (activeQuests [i].Active == true)
					activeQuests [i].Update ();		//find a way to make the update function in the Script run by itself!

				if (activeQuests [i].Complete == true)
				{
					AddToComplete (activeQuests [i]);
					activeQuests [i] = null;
				}
			}
		}

		for (int y = 0; y < completedQuests.Length; y++)
		{
			if (completedQuests [y] != null)
			{
				if (completedQuests [y].Complete && !completedQuests [y].Counted)
				{
					questsComplete++;
					completedQuests [y].Counted = true;
					Debug.Log ("called");
				}
			}
		}

		if (questsComplete >= totalQuests)
		{
			Debug.Log ("you won the game");
		}
	}

	void AddToComplete (Quest quest)
	{
		for (int x = 0; x < completedQuests.Length; x++)
		{
			if (completedQuests [x] != quest)
			{
				completedQuests [x] = quest;
			}
		}
	}
}
