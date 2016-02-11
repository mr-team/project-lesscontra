using UnityEngine;
using UnityEditor;
using System.Collections;

public class QuestEditor : EditorWindow
{
	string myString = "hello window";
	bool groupEnabled;
	bool fadeGroupEnabeled;

	bool myBool = true;
	float myFloat = 1.24f;

	[MenuItem ("Window/QuestEditor")]

	public static void ShowWindow ()
	{
		EditorWindow.GetWindow (typeof(QuestEditor));

	}

	void OnGUI ()
	{ 
		GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
		myString = EditorGUILayout.TextField ("Text Field", myString);

		groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);

		myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
		EditorGUILayout.BeginFadeGroup (1);
		myBool = EditorGUILayout.Toggle ("Toggle", myBool);
		EditorGUILayout.EndFadeGroup ();
		EditorGUILayout.EndToggleGroup ();
		
	}
}
