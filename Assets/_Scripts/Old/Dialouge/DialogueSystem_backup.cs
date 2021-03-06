using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogueSystemfdsahlkdslkfj : MonoBehaviour
{
	Actor actor;
	[HideInInspector]
	public Canvas dialogueCanvas;
	Text canvasText;
	GameObject dialogueButtons;
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
			DisplayDialouge (dialogueText [0]);
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

	void OnMouseDown ()
	{
		active = true;
		if (active)
			goToNextWindow = true;
	}

}
