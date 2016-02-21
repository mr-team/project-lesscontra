using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

public class ActorEditor : EditorWindow
{
	protected List <GameObject> savedActorObjects = new List<GameObject> ();
	protected GameObject selectedObject;
	protected Actor selectedActor;

	[MenuItem ("Window/ActorEditor")]

	public static void ShowWindow ()
	{
		ActorEditor actorEditor = (ActorEditor)EditorWindow.GetWindow (typeof(ActorEditor));
		actorEditor.Show ();
	}

	void OnGUI ()
	{
		if (Selection.activeGameObject == null || Selection.activeGameObject.GetComponent<NPC_Generic> () == null)//no actor selected, selection mode
		{
			GUILayout.Label ("Please select and Actor in the Hierarchy", EditorStyles.boldLabel);
		}
		if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<NPC_Generic> () != null) //Actor selected, editor mode
		{
			selectedActor = Selection.activeGameObject.GetComponent <NPC_Generic> ();

			GUILayout.Label ("Currently editing: " + Selection.activeGameObject.name, EditorStyles.boldLabel);
			GUILayout.BeginArea (new Rect (25, 25, 200, 500), EditorStyles.);
			EditorGUILayout.Space ();
			GUILayout.Label ("Edit Atributes: " + Selection.activeGameObject.name, EditorStyles.boldLabel);
			selectedActor.atributes.actorName = EditorGUILayout.TextField (selectedActor.name);
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
			GUILayout.EndArea ();
		}
	}
}
