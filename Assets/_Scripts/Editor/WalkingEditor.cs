using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(GuardController))]
public class WalkingEditor : Editor
{

	/*private Vector3 minVector = new Vector3 (float.MinValue, float.MinValue, float.MinValue);

	void OnSceneGUI ()
	{
		GuardController master = (GuardController)target;
		Vector3 lastPos = master.guardActions [master.guardActions.Count - 1].walkToPoint != minVector ?
                            master.guardActions [master.guardActions.Count - 1].walkToPoint : master.GetComponent<Transform> ().position;
		if (master.guardActions.Count == 0)
			return;
		for (int i = 0; i < master.guardActions.Count; i++)
		{
			GuardController.NPCAction current = master.guardActions [i];
			if (current.name == "Walking")
			{
				Handles.color = Color.green;
				Handles.DrawLine (lastPos, current.walkToPoint);
				Handles.DrawSolidDisc (current.walkToPoint, Vector3.up, .2f);
				Handles.Label (current.walkToPoint, (i + 1) + ". Wait: " + current.wait + " sec");
				lastPos = current.walkToPoint;
			} else
			{
				Handles.color = Color.red;
				Handles.DrawSolidDisc (lastPos, Vector3.up, .1f);
				Handles.Label (new Vector3 (lastPos.x, lastPos.y - 1f, lastPos.z), (i + 1) + ". Wait: " + current.wait + " sec - Animation: " + current.whatAnimation);
			}
			EditorUtility.SetDirty (target);
		}
	}

	public override void OnInspectorGUI ()
	{
		GuardController master = (GuardController)target;
		DrawDefaultInspector ();
		bool dontUpdate = false;
		for (int i = 0; i < master.guardActions.Count; i++)
		{
			EditorGUILayout.BeginHorizontal ();
			GuardController.NPCAction current = master.guardActions [i];
			EditorGUILayout.LabelField ((i + 1) + ". " + current.name);
			if (GUILayout.Button ("-", GUILayout.MaxWidth (20), GUILayout.MaxHeight (20)))
			{
				master.guardActions.Remove (current);
				dontUpdate = true;
			}
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.Space ();
			current.wait = EditorGUILayout.FloatField ("Wait", current.wait);
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.Space ();
			if (current.name == "Walking")
			{
				current.walkToPoint = EditorGUILayout.Vector3Field ("Destination", current.walkToPoint);
				if (GUILayout.Button ("*", GUILayout.MaxWidth (15), GUILayout.MaxHeight (15)))
				{
					current.walkToPoint = Vector3.zero;
				}
			} else
			{
				current.whatAnimation = EditorGUILayout.IntField ("Animation ID", current.whatAnimation);
			}
			EditorGUILayout.EndHorizontal ();
			GUILayout.Label ("");
			if (!dontUpdate)
				master.guardActions [i] = current;
		}
		if (GUILayout.Button ("Walk point"))
		{
			Vector3 point = master.transform.position;
			master.guardActions.Add (new GuardController.NPCAction (point));
		}
		if (GUILayout.Button ("Animation"))
		{
			master.guardActions.Add (new GuardController.NPCAction (new Animator ()));
		}
		EditorUtility.SetDirty (target);
	}*/

}