using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.IO;
using System.Text;

public class ActorEditor : EditorWindow
{
	string actorName;

	
	[MenuItem ("Window/ActorEditor")]

	public static void ShowWindow ()
	{
		EditorWindow.GetWindow (typeof(ActorEditor));

	}

	void OnGUI ()
	{
		GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
	}
}
