using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
	[HideInInspector]
	public Canvas dialogueCanvas;
	Text canvasText;

	[HideInInspector]
	public List<string> dialogueText = new List<string> ();
	string currentDialWindow;

	[HideInInspector]
	public bool active;
	[HideInInspector]
	public bool goToNextWindow;
	bool isTalking;
	bool initialise = true;
	bool endDialouge;

	int i = 1;

	void Awake ()
	{	
		dialogueCanvas = GameObject.Find ("DialougeCanvas").GetComponent<Canvas> ();
		canvasText = dialogueCanvas.GetComponentInChildren<Text> ();

		dialogueCanvas.gameObject.SetActive (false);
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.K))
			active = true;
		if (Input.GetKeyDown (KeyCode.K) && active)
			goToNextWindow = true;
		
		if (active)
		{
			dialogueCanvas.gameObject.SetActive (true);
			DisplayDialouge (dialogueText [0]);

		}
	}

	void ConformText ()
	{
		
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
				active = false;
				initialise = true;
				endDialouge = false;
				dialogueCanvas.gameObject.SetActive (false);
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
			i = 1;
			endDialouge = true;

		}

		if (i < dialogueText.Count) //prevent an argument out of range
			currentDialWindow = dialogueText [i];	
		
		i++;

		return currentDialWindow;
	}

	/// <summary>
	/// adds dialouge to the string list for debuging
	/// </summary>
	/*void DebugAddText ()
	{
		string dialWindow1 = "Hei im dankfart, i run this fucking mill. You might think this is some easy peasy job but fuck you";
		string dialWindow2 = "dialWindow2";
		string dialWindow3 = "dialWindow3";
		string dialWindow4 = "dialWindow4";
		string dialWindow5 = "dialWindow5";

		dialougeText.Add (dialWindow1);
		dialougeText.Add (dialWindow2);
		dialougeText.Add (dialWindow3);
		dialougeText.Add (dialWindow4);
		dialougeText.Add (dialWindow5);
	}*/
}
