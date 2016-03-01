using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GuardController))]
public class GuardActionEditor : Editor {

    private Vector3 minVector = new Vector3(float.MinValue, float.MinValue, float.MinValue);

    void OnSceneGUI() {
        GuardController master = (GuardController) target;
        Vector3 lastPos = master.guardActions[master.guardActions.Count - 1].type != NPCAction.ActionType.Animation ?
            master.guardActions[master.guardActions.Count - 1].dest : master.GetComponent<Transform>().position;
        if(master.guardActions.Count == 0)
            return;
        for(int i = 0; i < master.guardActions.Count; i++) {
            NPCAction current = master.guardActions[i];
            if(current.type == NPCAction.ActionType.Walking) {
                Handles.color = Color.green;
                Handles.DrawLine(lastPos, current.dest);
                Handles.DrawSolidDisc(current.dest, Vector3.up, .2f);
                Handles.Label(current.dest, (i + 1) + ". Wait: " + current.waitTime + " sec");
                lastPos = current.dest;
            } else {
                Handles.color = Color.red;
                Handles.DrawSolidDisc(lastPos, Vector3.up, .1f);
                Handles.Label(new Vector3(lastPos.x, lastPos.y - 1f, lastPos.z), (i + 1) + ". Wait: " + current.waitTime + " sec - Animation: " + current.ani);
            }
            EditorUtility.SetDirty(target);
        }
    }

    public override void OnInspectorGUI() {
        GuardController master = (GuardController) target;
        DrawDefaultInspector();
        bool dontUpdate = false;
        for(int i = 0; i < master.guardActions.Count; i++) {
            EditorGUILayout.BeginHorizontal();
            NPCAction current = master.guardActions[i];
            EditorGUILayout.LabelField((i + 1) + ". " + current.type);
            if(GUILayout.Button("-", GUILayout.MaxWidth(20), GUILayout.MaxHeight(20))) {
                master.guardActions.Remove(current);
                dontUpdate = true;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            current.waitTime = EditorGUILayout.FloatField("Wait", current.waitTime);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if(current.type == NPCAction.ActionType.Walking) {
                current.dest = EditorGUILayout.Vector3Field("Destination", current.dest);
                if(GUILayout.Button("*", GUILayout.MaxWidth(15), GUILayout.MaxHeight(15))) {
                    current.dest = Vector3.zero;
                }
            } else {
                current.ani = EditorGUILayout.IntField("Animation ID", current.ani);
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Label("");
            if(!dontUpdate)
                master.guardActions[i] = current;
        }
        if(GUILayout.Button("Walk point")) {
            Vector3 point = master.transform.position;
            master.guardActions.Add(new NPCAction(point));
        }
        if(GUILayout.Button("Animation")) {
            master.guardActions.Add(new NPCAction(0));
        }
        EditorUtility.SetDirty(target);
    }
}
