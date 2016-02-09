using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CanIWalkNow : MonoBehaviour {
    [HideInInspector]
    public List<NPCAction> guardActions = new List<NPCAction>();

    private int onCurrentAction = 0;

    void Awake() {
        NPCAction walk = new NPCAction(Vector3.zero);
        guardActions.Add(walk);
    }

    void FixedUpdate() {
        if(guardActions.Count == 0)
            return;
        NPCAction dis = guardActions[onCurrentAction];
        Vector3.MoveTowards(transform.position, dis.walkToPoint, 1f);
    }

    [System.Serializable]
    public struct NPCAction {
        public float wait;
        public Vector3 walkToPoint;
        public Animator runAnimation;
        public int whatAnimation;
        public string name;

        public NPCAction(Vector3 point, float _wait = 1f) {
            wait = _wait;
            walkToPoint = point;
            runAnimation = null;
            whatAnimation = 0;
            name = "Walking";
        }

        public NPCAction(Animator ani, int animationNumber = 0, float _wait = 1f) {
            wait = _wait;
            runAnimation = ani;
            whatAnimation = animationNumber;
            walkToPoint = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            name = "Animation";
        }
    }
    
}