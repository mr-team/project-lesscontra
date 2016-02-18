using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(NpcController))]
public class Actor : MonoBehaviour
{
	public List<NPCAction> actorActions = new List<NPCAction> ();
	protected NpcController actorControll;
	protected Test_DialougeScript dialouge;

	public string actorName;

	public bool isHostile = false;
	public bool isQuestGiver = false;
	public bool isEssential = false;
	public bool hasDialouge = false;
	public bool isMerchant = false;
	public bool isSkillMaster = false;

	protected float health;

	public float walkSpeed = 1.5f;
	public float turnSpeed = 2f;

	protected int onCurrentAction = 0;
	protected float waitTimer = 0f;

	private float closeEnoughLimit = 0.2f;
	private Vector3 _direction;
	private Quaternion _lookRotation;

	protected virtual void Awake ()
	{
		if (isHostile)
		{
			isQuestGiver = false;
			isEssential = false;
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
		if (actorName == "")
			Debug.LogError ("The actor " + gameObject + " has no name");
		
		if (actorControll == null)
			Debug.LogError ("The actor " + gameObject + " has not controller");

		if (hasDialouge)
		{
			gameObject.AddComponent<Test_DialougeScript> ();
			dialouge = GetComponent<Test_DialougeScript> ();
		}
		if (isMerchant)
		{
			
		}
	}

	[System.Serializable]
	public struct NPCAction
	{
		public float wait;
		public Vector3 walkToPoint;
		public Animator runAnimation;
		public int whatAnimation;
		public string name;

		public NPCAction (Vector3 point, float _wait = 1f)
		{
			wait = _wait;
			walkToPoint = point;
			runAnimation = null;
			whatAnimation = 0;
			name = "Walking";
		}

		public NPCAction (Animator ani, int animationNumber = 0, float _wait = 1f)
		{
			wait = _wait;
			runAnimation = ani;
			whatAnimation = animationNumber;
			walkToPoint = new Vector3 (float.MinValue, float.MinValue, float.MinValue);
			name = "Animation";
		}
	}

	protected bool nextAction ()
	{
		int actions = actorActions.Count - 1;
		if (onCurrentAction == actions)
		{
			onCurrentAction = 0;
			return false;
		}
		onCurrentAction++;
		return true;
	}

	protected void DoAction (NPCAction action)
	{
		if (action.name == "Walking")
		{
			if (Vector3.Distance (transform.position, action.walkToPoint) >= closeEnoughLimit)
			{
				_direction = (action.walkToPoint - transform.position).normalized;
				_lookRotation = Quaternion.LookRotation (_direction);
				transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.fixedDeltaTime * turnSpeed);
				transform.position = Vector3.MoveTowards (transform.position, action.walkToPoint, Time.fixedDeltaTime * walkSpeed);
			} else
			{
				waitTimer += Time.fixedDeltaTime;
				if (waitTimer >= action.wait)
				{
					nextAction ();
					waitTimer = 0f;
				}
			}
		}
	}
}
