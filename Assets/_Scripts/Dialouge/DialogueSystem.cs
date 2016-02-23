using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
	Actor actor;
	[HideInInspector]
	public Canvas dialogueCanvas;
	Text canvasText;
	[SerializeField]
	public List<string> dialogueText = new List<string> ();
	[SerializeField]
	public string[] dialogueTextArray = new string[0];
	string currentDialWindow;

	[HideInInspector]
	public bool active;
	[HideInInspector]
	public bool goToNextWindow;
	bool isTalking;
	[HideInInspector]
	public bool initialise = true;
	bool endDialouge;
	bool saved;
	int i = 0;

	void Awake ()
	{	
		saved = false;
		actor = gameObject.GetComponent<Actor> ();
		dialogueCanvas = GameObject.Find ("DialougeCanvas").GetComponent<Canvas> ();
		canvasText = dialogueCanvas.GetComponentInChildren<Text> ();
		dialogueCanvas.gameObject.SetActive (false);
	}

	void Update ()
	{
		
		if (active)
		{
			dialogueCanvas.gameObject.SetActive (true);
			DisplayDialouge (dialogueText [0]);
		}
	}

	/// <summary>
	/// Displays the dialouge on the canvas.
	/// </summary>
	/// <param name="startString">Start string.</param>
	void DisplayDialouge (string startString)
	{
		if (initialise)
		{
			canvasText.text = startString;
			currentDialWindow = startString;
			initialise = false;
		}

		if (goToNextWindow)
		{
			canvasText.text = DisplayNextWindow ();
			if (endDialouge)
			{
				EndDialogue ();
			}
			goToNextWindow = false;
		}
	}

	/// <summary>
	/// /Displays the next window of the dialouge.*/
	/// </summary>
	/// <returns>The next window.</returns>
	string DisplayNextWindow ()
	{
		if (i >= dialogueText.Count)
		{
			endDialouge = true;
		}

		if (!endDialouge)
		{
			if (i < dialogueText.Count) //prevent an argument out of range
				currentDialWindow = dialogueText [i];	
			i++;
			return currentDialWindow;
		}
		return currentDialWindow;
	}

	public void EndDialogue ()
	{
		active = false;
		initialise = true;
		endDialouge = false;
		i = 0;
		dialogueCanvas.gameObject.SetActive (false);
	}
}
