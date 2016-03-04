using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{

	public Canvas dialougeCanvas;
	public GameObject UIObject;
	public GameObject player;

	[HideInInspector]
	public PlayerController playerControll;
	//UIFeedBack UIfeedBack;

	public Action<Vector3> CallUI_WalkPoint;

	void Awake ()
	{
		//UIfeedBack = UIObject.GetComponent<UIFeedBack> ();
		//playerControll = player.GetComponent<PlayerController> ();

		//CallUI_WalkPoint = UIfeedBack.WalkPointAnim;

	}

	void Update ()
	{
	
	}
}
