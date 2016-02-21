using UnityEngine;
using System.Collections;

public class NPC_Generic : Actor
{
	
	public bool canActivateDialogue;

	protected override void Awake ()
	{
		
		base.Awake ();
	}

	void Update ()
	{
		if (actorActions.Count != 0)
			DoAction (actorActions [onCurrentAction]);
	}

	void OnMouseDown ()
	{
		if (!controller.playerControll.FPModeActive && canActivateDialogue)
		{
			if (atributes.hasDialouge && canActivateDialogue)
			{
				if (dialouge.dialogueText.Count != 0)
				{
					dialouge.goToNextWindow = false;
					dialouge.initialise = true;
					dialouge.active = true;
				}

				if (dialouge.active)
					dialouge.goToNextWindow = true;
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.transform.tag == "Player")
			canActivateDialogue = true;

		if (other == null)
			canActivateDialogue = false;
	}

	void OnTriggerExit (Collider other)
	{
		if (other.transform.tag == "Player")
		{
			dialouge.EndDialogue ();
			canActivateDialogue = false;
		}
			
	}
}
