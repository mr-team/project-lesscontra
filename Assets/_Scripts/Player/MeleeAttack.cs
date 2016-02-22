using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeAttack : MonoBehaviour {
    [Range(0, 100)]
    public float minDamage = 100f;
    [Range(0, 100)]
    public float maxDamage = 100f;
    public int listCount;

    private List<NPC_Generic> targets = new List<NPC_Generic>();
    private int closestTargetIndex;
    private float closestTargetDistance = float.MaxValue;

    public void addTarget(NPC_Generic npc) {
        targets.Add(npc);
    }

    public List<NPC_Generic> getTargets() {
        return targets;
    }

    private void updateClosestTarget() {
        listCount = 0;
        for(int i = 0; i < targets.Count; i++) {
            Vector3 pos = targets[i].transform.position;
            Vector3 playerPos = this.transform.position;
            float distance = Vector3.Distance(pos, playerPos);
            listCount++;
            if(distance < closestTargetDistance) {
                distance = closestTargetDistance;
                closestTargetIndex = i;
            }
        }
    }

    private void attack() {
        Transform target = targets[closestTargetIndex].transform;
        target.GetComponent<ActorStats>().health -= Random.Range(minDamage, maxDamage);
        targets = null;
    }
	
	void FixedUpdate () {
        if(targets.Count != 0) {
            updateClosestTarget();
            if(closestTargetDistance <= 0.5f) {
                attack();
            }
        }
	}
}
