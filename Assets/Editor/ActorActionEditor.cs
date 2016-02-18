using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(NPC_Generic))]
public class ActorActionEditor : Editor
{

	private Vector3 minVector = new Vector3 (float.MinValue, float.MinValue, float.MinValue);

	void OnSceneGUI ()
	{
		Actor master = (Actor)target;
		Vector3 lastPos = master.actorActions [master.actorActions.Count - 1].walkToPoint != minVector ?
			master.actorActions [master.actorActions.Count - 1].walkToPoint : master.GetComponent<Transform> ().position;
		if (master.actorActions.Count == 0)
			return;
		for (int i = 0; i < master.actorActions.Count; i++)
		{
			Actor.NPCAction current = master.actorActions [i];
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
		Actor master = (Actor)target;
		DrawDefaultInspector ();
		bool dontUpdate = false;
		for (int i = 0; i < master.actorActions.Count; i++)
		{
			EditorGUILayout.BeginHorizontal ();
			Actor.NPCAction current = master.actorActions [i];
			EditorGUILayout.LabelField ((i + 1) + ". " + current.name);
			if (GUILayout.Button ("-", GUILayout.MaxWidth (20), GUILayout.MaxHeight (20)))
			{
				master.actorActions.Remove (current);
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
				master.actorActions [i] = current;
		}
		if (GUILayout.Button ("Walk point"))
		{
			Vector3 point = master.transform.position;
			master.actorActions.Add (new Actor.NPCAction (point));
		}
		if (GUILayout.Button ("Animation"))
		{
			master.actorActions.Add (new Actor.NPCAction (new Animator ()));
		}
		EditorUtility.SetDirty (target);
	}
}
