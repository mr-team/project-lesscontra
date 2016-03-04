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
	GameObject dialogueButtons;
	[SerializeField]
	public List<DialogueWindow> dialogueWindow = new List<DialogueWindow> ();
	string currentDialWindowText;

	[HideInInspector]
	public bool active;
	[HideInInspector]
	public bool goToNextWindow;
	bool isTalking;
	[HideInInspector]
	public bool initialise = true;
	bool endDialouge;
	bool question;
	int i = 0;

	void Awake ()
	{	
		actor = gameObject.GetComponent<Actor> ();
		dialogueCanvas = GameObject.Find ("DialogueCanvas").GetComponent<Canvas> ();
		canvasText = GameObject.Find ("DialogueText").GetComponent<Text> ();
		dialogueButtons = GameObject.Find ("DialogueButtons");
		dialogueButtons.SetActive (false);
		dialogueCanvas.gameObject.SetActive (false);

	}

	void Update ()
	{
		if (active)
		{
			dialogueCanvas.gameObject.SetActive (true);
			DisplayDialouge (dialogueWindow [0].DialogueText);
			if (question)
			{
				dialogueButtons.SetActive (true);
			}
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
			currentDialWindowText = startString;
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
		if (i >= dialogueWindow.Count)
		{
			endDialouge = true;
		} else if (i != 0 && dialogueWindow [i - 1].LastWindow)
		{
			endDialouge = true;
		}
		if (!endDialouge)
		{
			if (i < dialogueWindow.Count) //prevent an argument out of range
				currentDialWindowText = dialogueWindow [i].DialogueText;	
			i++;
			return currentDialWindowText;
		}
		return currentDialWindowText;
	}

	public void EndDialogue ()
	{
		active = false;
		initialise = true;
		endDialouge = false;
		i = 0;
		dialogueCanvas.gameObject.SetActive (false);
	}

	void OnMouseDown ()
	{
		active = true;
		if (active)
			goToNextWindow = true;
	}

}
