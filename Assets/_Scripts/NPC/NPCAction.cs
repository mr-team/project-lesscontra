using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class NPCAction {
    public enum ActionType {
        Walking, Animation
    }

    public Vector3 dest;
    public int ani;
    public float waitTime;
    public ActionType type;

    public NPCAction(Vector3 _dest, float _waitTime = 0f) {
        dest = _dest;
        ani = 0;
        waitTime = _waitTime;
        type = ActionType.Walking;
    }

    public NPCAction(int _ani, float _waitTime = 0f) {
        dest = Vector3.zero;
        ani = _ani;
        waitTime = _waitTime;
        type = ActionType.Animation;
    }
}
