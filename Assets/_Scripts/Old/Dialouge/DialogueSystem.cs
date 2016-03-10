using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
	[SerializeField]
	public Quest placeholderQuest;
	QuestManager QuestManager;
	Actor actor;
	[HideInInspector]
	public Canvas dialogueCanvas;
	Text canvasText;
	GameObject dialogueButtonParent;
	Button[] dialogueButtons = new Button[2];
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
		if (GameObject.Find ("Player") == null)
			Debug.LogError ("There is no Gamobject named Player in the scene");
		else if (GameObject.Find ("Player") != null)
		{
			QuestManager = GameObject.Find ("Player").GetComponent<QuestManager> ();

		}

		actor = gameObject.GetComponent<Actor> ();
		dialogueCanvas = GameObject.Find ("DialogueCanvas").GetComponent<Canvas> ();
		canvasText = GameObject.Find ("DialogueText").GetComponent<Text> ();
		dialogueButtonParent = GameObject.Find ("DialogueButtons");

		dialogueButtons [0] = GameObject.Find ("Button_Yes").GetComponent<Button> ();
		dialogueButtons [1] = GameObject.Find ("Button_No").GetComponent<Button> ();

		//delegates the function of the active dialogue to the two buttons
		dialogueButtons [0].GetComponent<Button> ().onClick.AddListener (delegate
		{
			OnAnswerYes ();
		});
		dialogueButtons [1].GetComponent<Button> ().onClick.AddListener (delegate
		{
			OnAnswerNo ();
		});

		dialogueButtonParent.SetActive (false);
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
				dialogueButtonParent.SetActive (true);
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

			if (i != 0 && dialogueWindow [i - 1].IsQuestion)
			{
				dialogueButtonParent.SetActive (true);
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

	void OnAnswerYes ()
	{
		//brute force
		Quest placeholder_Quest = new Quest (actor);
		placeholder_Quest.Objectives = new Objective[1];

		placeholder_Quest.Objectives [0] = new Objective (GameObject.Find ("GenActKill").GetComponent<Actor> ());
		placeholder_Quest.Active = true;


		QuestManager.activeQuests [0] = placeholder_Quest;
	}

	void OnAnswerNo ()
	{
		endDialouge = true;
	}


}
