using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.IO;
using System.Text;

public class ActorEditor : EditorWindow
{
	protected Actor selectedActor;

	[MenuItem ("Window/ActorEditor")]

	public static void ShowWindow ()
	{
		EditorWindow.GetWindow (typeof(ActorEditor));

	}

	void OnGUI ()
	{

		if (Selection.activeGameObject == null || Selection.activeGameObject.GetComponent<NPC_Generic> () == null)
		{
			GUILayout.Label ("Please select and Actor in the Hierarchy", EditorStyles.boldLabel);

		}
		if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<NPC_Generic> () != null)
		{
			selectedActor = Selection.activeGameObject.GetComponent <NPC_Generic> ();

			GUILayout.Label ("Currently editing: " + Selection.activeGameObject.name, EditorStyles.boldLabel);
			selectedActor.name = EditorGUILayout.TextField (selectedActor.name);
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
			GUILayout.Label ("Currently editing: " + Selection.activeGameObject.name, EditorStyles.boldLabel);
		}
	}
}
