using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.IO;

public class DialougeEditor : EditorWindow
{
	protected Vector2 scrollPositon = Vector2.zero;
	protected bool saved;
	protected bool FirstTimeOfSession = true;
	protected GameObject selectedObject;
	protected Actor selectedActor;
	protected DialogueSystem dialogue;
	protected string path;
	protected int windowNum;
	protected String[] jumpToOptions = new string[2];
	protected int index;
	protected bool startRecord;

	[MenuItem ("Window/DialogueEditor")]
	public static void ShowWindow ()
	{
		DialougeEditor dialogueEditor = (DialougeEditor)EditorWindow.GetWindow (typeof(DialougeEditor));
		dialogueEditor.ShowPopup ();
	}

	public void OnGUI ()
	{
		selectedObject = Selection.activeGameObject;

		if (!Application.isPlaying)
		{
			saved = false;

			if (selectedObject == null || selectedObject.GetComponent<NPC_Generic> () == null) //no actor selected
			{
				GUILayout.Label ("Please select and Actor in the Hierarchy", EditorStyles.boldLabel);
			}

			if (selectedObject != null && selectedObject.GetComponent<NPC_Generic> () != null) //if the selected gameObject is an actor
			{
				selectedActor = selectedObject.GetComponent <NPC_Generic> ();

				if (selectedObject.GetComponent<DialogueSystem> () == null) //has no dialouge, enter create mode
				{
					GUILayout.Label ("The selected Actor " + selectedObject.name + " has no dialouge", EditorStyles.boldLabel);
					if (GUILayout.Button ("Create Dialouge"))
					{
						selectedObject.AddComponent<DialogueSystem> ();
						dialogue = selectedObject.GetComponent<DialogueSystem> ();
						selectedActor.atributes.hasDialouge = true;
					}
				}

				//EDIT MODE
				if (Selection.activeGameObject.GetComponent<DialogueSystem> () != null) //has dialouge, enter edit mode
				{
					dialogue = selectedObject.GetComponent<DialogueSystem> ();
					EditorGUILayout.BeginHorizontal ();
					GUILayout.Label ("Currently editing dialouge for: " + selectedObject.name, EditorStyles.boldLabel);
					if (GUILayout.Button ("deleate dialouge System"))
					{
						windowNum = 0;
						selectedActor.dialouge = null;
						selectedActor.atributes.hasDialouge = false;
						ClearSave ();
						DestroyImmediate (selectedObject.GetComponent<DialogueSystem> ());
					}
					if (FirstTimeOfSession)
					{
						if (File.Exists (path + "/Dialogue/" + selectedObject.name + ".txt"))
						{
							LoadDialouge ();
						}
						FirstTimeOfSession = false;

					}
					EditorGUILayout.EndHorizontal ();
					EditorGUILayout.Space ();
					scrollPositon = EditorGUILayout.BeginScrollView (scrollPositon);

					for (int i = 0; i < dialogue.dialogueWindow.Count; i++)	//the draw loop
					{
						DialogueWindow current = dialogue.dialogueWindow [i];	//set the current dialogue window
						current.WindowNum = i;	//fix the dialogue window winNUm
						EditorGUILayout.Space ();
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Dialouge window: " + (i + 1));
			
						if (dialogue.dialogueWindow.Count > 1)
						{
							if (GUILayout.Button ("x", GUILayout.Height (20), GUILayout.Width (20)))
							{
								windowNum--;
								dialogue.dialogueWindow.Remove (current);
								return;
							}
						}
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.BeginHorizontal ();
						if (i + 1 == dialogue.dialogueWindow.Count)
							dialogue.dialogueWindow [i].LastWindow = true;
						else
							dialogue.dialogueWindow [i].LastWindow = false;
						
						if (dialogue.dialogueWindow [i].LastWindow)
						{
							if (GUILayout.Toggle (current.IsQuestion, "is this a question"))
							{
								current.IsQuestion = true;

								//index = EditorGUI.Foldout()

							} else
							{
								current.IsQuestion = false;
							}
						}
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.Space ();
						dialogue.dialogueWindow [i].DialogueText = EditorGUILayout.TextArea (dialogue.dialogueWindow [i].DialogueText, GUILayout.Height (50));


					} //draw loop end

					EditorGUILayout.Space ();

					if (GUILayout.Button ("New Window"))
					{
						dialogue.dialogueWindow.Add (new DialogueWindow ("", 0)); //create a new dialogue window with an empty string and a defult windownum of 0 (the draw loop takes care of the winNum)
					}

					EditorGUILayout.Space ();
					EditorGUILayout.EndScrollView ();
					EditorGUILayout.BeginHorizontal ();
					if (GUILayout.Button ("save Dialogue"))
					{
						SaveDialogue ();
						saved = true;
						GUI.FocusControl (null);
						return;
					}
					if (File.Exists (Application.dataPath + "/Dialogue/" + selectedObject.name + ".txt"))
					{
						if (GUILayout.Button ("load Dialogue"))
						{
							LoadDialouge ();
							GUI.FocusControl (null);
							return;
						}
					}
				
					EditorGUILayout.EndHorizontal ();
				}
			}
		}

		if (Application.isPlaying)
		{
			EditorGUILayout.Space ();
			GUILayout.Label ("Editing dialogue is disabled in playmode", EditorStyles.boldLabel);
		}
	}

	void OnDestroy ()
	{
		FirstTimeOfSession = true;
	}

	public void SaveDialogue ()
	{
		;
		string tempSave = "";

		for (int i = 0; i < dialogue.dialogueWindow.Count; i++)
		{
			tempSave += (Environment.NewLine + "//Window " + (i + 1)
			+ Environment.NewLine
			+ ("/w ") + dialogue.dialogueWindow [i].DialogueText
			+ Environment.NewLine
			);
		}
		if (!Directory.Exists (Application.dataPath + "/Dialogue"))	//if the directory does not exist...
			Directory.CreateDirectory (Application.dataPath + "/Dialogue");	//...create it
		
		if (!File.Exists (Application.dataPath + "/Dialogue/" + selectedObject.name + ".txt")) 		// if a save for this object does not exits...
			File.CreateText (Application.dataPath + "/Dialogue/" + selectedObject.name + ".txt").Dispose ();	//..Create it
		
		if (File.Exists (Application.dataPath + "/Dialogue/" + selectedObject.name + ".txt")) //if a save file for this object exists...
		{
			File.WriteAllText (Application.dataPath + "/Dialogue/" + selectedObject.name + ".txt",	//...write to it
				("Dialogue file for Actor " + selectedObject.name
				+ Environment.NewLine
				+ tempSave
				+ Environment.NewLine));
		}
	}

	public void LoadDialouge ()
	{
		
		string[] tempLoad;

		tempLoad = File.ReadAllLines (Application.dataPath + "/Dialogue/" + selectedObject.name + ".txt");

		dialogue.dialogueWindow.Clear ();

		int x = 1;

		for (int i = 0; i < tempLoad.Length; i++)
		{
			char[] tempCharA = (tempLoad [i].ToCharArray ());

			if (tempCharA.Length > 0 && tempLoad [i].Substring (0, 3) == "/w ")
			{
				dialogue.dialogueWindow.Add (new DialogueWindow (tempLoad [i].Remove (0, 3), x));
				x++;
			}
		}
	}

	public void ClearSave ()
	{
		
		File.Delete (Application.dataPath + "/Dialogue/" + selectedObject.name + ".txt");
	}
}