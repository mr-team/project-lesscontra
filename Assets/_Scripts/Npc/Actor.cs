using UnityEngine;
using System.Collections;

[RequireComponent (typeof(NpcController))]
public class Actor : MonoBehaviour
{
	protected NpcController actorControll;
	protected string actorName;

	protected bool isHostile = true;
	protected bool isQuestGiver = true;
	protected bool essential = true;
	protected bool hasDialouge = true;
	protected bool isMerchant = true;
	protected bool isSkillMaster = true;

	protected virtual void Awake ()
	{
		if (isHostile)
		{
			isQuestGiver = false;
			essential = false;
			hasDialouge = false;
			isMerchant = false;
			isSkillMaster = false;

		} 
		if (isMerchant)
			isSkillMaster = false;

		actorControll = GetComponent<NpcController> ();
		CheckSetup ();
	}

	void CheckSetup ()
	{
		if (actorName == null)
			Debug.LogError ("The actor " + gameObject + " has no name");
		
		if (actorControll == null)
			Debug.LogError ("The actor " + gameObject + " has not controller");
	}
}
