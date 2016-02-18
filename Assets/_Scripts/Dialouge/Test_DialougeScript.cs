using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Test_DialougeScript : MonoBehaviour
{
	public Canvas dialougeCanvas;
	Text canvasText;
	
	public List<string> dialougeText;
	string currentDialWindow;

	public bool active;
	public bool goToNextWindow;
	bool isTalking;
	bool initialise = true;
	bool endDialouge;

	int i = 0;

	void Awake ()
	{	
		dialougeText = new List<string> ();
		DebugAddText ();
		dialougeCanvas = GameObject.Find ("DialougeCanvas").GetComponent<Canvas> ();
		canvasText = dialougeCanvas.GetComponentInChildren<Text> ();

		dialougeCanvas.gameObject.SetActive (false);
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.K))
			active = true;
		
		if (active)
			DisplayDialouge (dialougeText [0]); // display the canvas
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
		Debug.Log ("Update This shit");
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
				dialougeCanvas.gameObject.SetActive (false);
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
		if (i >= dialougeText.Count)
		{
			i = 0;
			endDialouge = true;

		}

		if (i < dialougeText.Count) //prevent an argument out of 
			currentDialWindow = dialougeText [i];	
		
		i++;

		return currentDialWindow;
	}

	/// <summary>
	/// adds dialouge to the string list for debuging
	/// </summary>
	void DebugAddText ()
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
	}
}
