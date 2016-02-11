using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CanIWalkNow : MonoBehaviour {
    [HideInInspector]
    public List<NPCAction> guardActions = new List<NPCAction>();

    public float walkSpeed = 1.5f;
    public float turnSpeed = 2f;

    private int onCurrentAction = 0;
    private float waitTimer = 0f;

    private float closeEnoughLimit = 0.2f;
    private Vector3 _direction;
    private Quaternion _lookRotation;

    bool nextAction() {
        int actions = guardActions.Count - 1;
        if(onCurrentAction == actions) {
            onCurrentAction = 0;
            return false;
        }
        onCurrentAction++;
        return true;
    }

    void FixedUpdate() {
        if(guardActions.Count == 0)
            return;
        NPCAction dis = guardActions[onCurrentAction];
        if(dis.name == "Walking") {
            if(Vector3.Distance(transform.position, dis.walkToPoint) >= closeEnoughLimit) {
                _direction = (dis.walkToPoint - transform.position).normalized;
                _lookRotation = Quaternion.LookRotation(_direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.fixedDeltaTime * turnSpeed);
                transform.position = Vector3.MoveTowards(transform.position, dis.walkToPoint, Time.fixedDeltaTime * walkSpeed);
            } else {
                waitTimer += Time.fixedDeltaTime;
                if(waitTimer >= dis.wait) {
                    nextAction();
                    waitTimer = 0f;
                }
            }
            
        }
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