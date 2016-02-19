using UnityEngine;
using System.Collections;
using UnityEditor;


public class DialougeEditor : EditorWindow
{

	protected GameObject selectedObject;
	protected Actor selectedActor;
	protected DialogueSystem dialogue;

	[MenuItem ("Window/DialogueEditor")]
	public static void ShowWindow ()
	{
		EditorWindow.GetWindow (typeof(DialougeEditor));

	}

	public void OnGUI ()
	{
		Debug.Log ("i ran");
		selectedObject = Selection.activeGameObject;
		
		if (selectedObject == null || selectedObject.GetComponent<NPC_Generic> () == null)
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

			if (Selection.activeGameObject.GetComponent<DialogueSystem> () != null) //has dialouge, enter edit mode
			{
				dialogue = selectedObject.GetComponent<DialogueSystem> ();
				EditorGUILayout.BeginHorizontal ();
				GUILayout.Label ("Currently editing dialouge for: " + selectedObject.name, EditorStyles.boldLabel);
				if (GUILayout.Button ("deleate dialouge"))
				{
					selectedActor.dialouge = null;
					selectedActor.atributes.hasDialouge = false;
					DestroyImmediate (selectedObject.GetComponent<DialogueSystem> ());
				}
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.Space ();


				if (dialogue.dialogueText.Count == 0)
				{
					EditorGUILayout.Space ();
					if (GUILayout.Button ("Start Dialouge"))
					{
						dialogue.dialogueText.Add ("");
					}
				}
				if (dialogue.dialogueText.Count != 0)
				{

					for (int i = 0; i < dialogue.dialogueText.Count; i++)
					{
						string current = dialogue.dialogueText [i];
						EditorGUILayout.Space ();
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Dialouge window: " + (i + 1));
						if (GUILayout.Button ("x", GUILayout.Height (20), GUILayout.Width (20)))
						{
							dialogue.dialogueText.Remove (current);
							return;
						}
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.Space ();
						dialogue.dialogueText [i] = EditorGUILayout.TextArea (current, GUILayout.Height (50));

					}
					EditorGUILayout.Space ();
					if (GUILayout.Button ("New Window"))
					{
						dialogue.dialogueText.Add ("");
					}
					EditorGUILayout.Space ();
					/*if (GUILayout.Button ("saveChanges"))
					{
						AssetDatabase.Refresh ();
					}*/
				}
			}
		}
	}
}