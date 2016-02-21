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
						dialogue.dialogueText.Add ("");
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
						selectedActor.dialouge = null;
						selectedActor.atributes.hasDialouge = false;
						ClearSave ();
						DestroyImmediate (selectedObject.GetComponent<DialogueSystem> ());
					}
					if (FirstTimeOfSession)
					{
						LoadDialouge ();
						FirstTimeOfSession = false;
					}
					EditorGUILayout.EndHorizontal ();
					EditorGUILayout.Space ();
					scrollPositon = EditorGUILayout.BeginScrollView (scrollPositon);
					for (int i = 0; i < dialogue.dialogueText.Count; i++)
					{
						string current = dialogue.dialogueText [i];
						EditorGUILayout.Space ();
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Dialouge window: " + (i + 1));

						if (dialogue.dialogueText.Count > 1)
						{
							if (GUILayout.Button ("x", GUILayout.Height (20), GUILayout.Width (20)))
							{
								dialogue.dialogueText.Remove (current);
								return;
							}
						}
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.Space ();
						dialogue.dialogueText [i] = EditorGUILayout.TextArea (dialogue.dialogueText [i], GUILayout.Height (50));

					}
					EditorGUILayout.Space ();


					if (GUILayout.Button ("New Window"))
					{
						dialogue.dialogueText.Add ("");
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
					if (File.Exists ("D:/Users/matias/Documents/UnityProjects/Spo1/project-lesscontra/Editor/Dialogue/" + selectedObject.name + ".txt"))
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
		string tempSave = "";

		for (int i = 0; i < dialogue.dialogueText.Count; i++)
		{
			tempSave += (Environment.NewLine + "//Windwo " + (i + 1)
			+ Environment.NewLine
			+ ("/w ") + dialogue.dialogueText [i]
			+ Environment.NewLine
			);
		}

		if (!File.Exists ("D:/Users/matias/Documents/UnityProjects/Spo1/project-lesscontra/Editor/Dialogue/" + selectedObject.name + ".txt")) 		// if a save for this object does not exits...
			File.CreateText ("D:/Users/matias/Documents/UnityProjects/Spo1/project-lesscontra/Editor/Dialogue/" + selectedObject.name + ".txt").Dispose ();	//..Create it
		
		if (File.Exists ("D:/Users/matias/Documents/UnityProjects/Spo1/project-lesscontra/Editor/Dialogue/" + selectedObject.name + ".txt")) //if a save file for this object exists
		{
			File.WriteAllText ("D:/Users/matias/Documents/UnityProjects/Spo1/project-lesscontra/Editor/Dialogue/" + selectedObject.name + ".txt",
				("Dialogue file for Actor " + selectedObject.name
				+ Environment.NewLine
				+ tempSave
				+ Environment.NewLine));
		}
	}

	public void LoadDialouge ()
	{
		string[] tempLoad;

		tempLoad = File.ReadAllLines ("D:/Users/matias/Documents/UnityProjects/Spo1/project-lesscontra/Editor/Dialogue/" + selectedObject.name + ".txt");

		dialogue.dialogueText.Clear ();

		int x = 0;
		for (int i = 0; i < tempLoad.Length; i++)
		{
			char[] tempCharA = (tempLoad [i].ToCharArray ());

			if (tempCharA.Length > 0 && tempLoad [i].Substring (0, 3) == "/w ")
			{

				dialogue.dialogueText.Add (tempLoad [i].Remove (0, 3));

				x++;
			}
		}
	}

	public void ClearSave ()
	{
		File.Delete ("D:/Users/matias/Documents/UnityProjects/Spo1/project-lesscontra/Editor/Dialogue/" + selectedObject.name + ".txt");
	}
}