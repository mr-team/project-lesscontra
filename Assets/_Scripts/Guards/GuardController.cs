using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuardController : MonoBehaviour {
    public GuardCommunicator GC;
    public GuardStats gs;
    public GuardState currentState = GuardState.Patrol;
    [HideInInspector]
    public List<NPCAction> guardActions = new List<NPCAction>();

    private NavMeshAgent agent;
    private PlayerController player;
    private float attackTimer = 0f;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        gs = GetComponent<GuardStats>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void FixedUpdate() {
        if(gs.health <= 0f) {
            Destroy(gameObject);
        }
        if(currentState == GuardState.Attacking) {
            if(Vector3.Distance(transform.position, player.transform.position) <= 1f) {
                Vector3 _direction = (player.transform.position - transform.position).normalized;
                Quaternion _lookRotation = Quaternion.LookRotation(_direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.fixedDeltaTime * 2f);

                if(attackTimer <= 0f) {
                    player.takeDamage(Random.Range(gs.minAttackDamage, gs.maxAttackDamage));
                    attackTimer = 2.5f;
                } else {
                    attackTimer -= Time.deltaTime;
                }

            } else {
                currentState = GuardState.Patrol;
            }
        } else if(currentState == GuardState.Alarmed) {
            if(agent.speed == 2.5f)
                agent.speed = 3f;
            if(Vector3.Distance(transform.position, GC.getLastKnownLocation()) <= 1f) {
                currentState = GuardState.Attacking;
            }
        } else if(currentState == GuardState.Patrol) {
            if(agent.speed != 2.5f)
                agent.speed = 2.5f;
            if(guardActions.Count != 0)
                DoAction(guardActions[onCurrentAction]);
        }

        if(GC.hasLastKnownLocation() && currentState != GuardState.Attacking && Vector3.Distance(transform.position, GC.getLastKnownLocation()) <= GC.alarmRadius) {
            currentState = GuardState.Alarmed;
            Vector3 lastLoc = GC.getLastKnownLocation();
            if(agent.destination != lastLoc)
                agent.SetDestination(GC.getLastKnownLocation());
        }
	}

    private float lookAroundCooldown = 1f;
    private void lookAround() {
        if(lookAroundCooldown > 0f) {
            lookAroundCooldown -= Time.deltaTime;
            Debug.Log("Lookaround cooldown");
            return;
        }
        lookAroundCooldown = 1f;
        Vector3 _direction = (transform.position - transform.position).normalized;
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.fixedDeltaTime * 2f);

        Debug.Log("Looked Around" + Time.deltaTime);
    }

    private int onCurrentAction = 0;
    private bool nextAction() {
        int actions = guardActions.Count - 1;
        if(onCurrentAction == actions) {
            onCurrentAction = 0;
            return false;
        }
        onCurrentAction++;
        return true;
    }

    private float closeEnoughLimit = 0.01f;
    private float waitTimer = 0f;
    private void DoAction(NPCAction action) {
        if(action.type == NPCAction.ActionType.Walking) {
            if(Vector3.Distance(transform.position, action.dest) >= closeEnoughLimit) {
                agent.SetDestination(action.dest);
            } else {
                waitTimer += Time.fixedDeltaTime;
                if(waitTimer >= action.waitTime) {
                    nextAction();
                    waitTimer = 0f;
                }
            }
        }
    }

    public enum GuardState {
        Patrol, Alarmed, Attacking
    }
}
