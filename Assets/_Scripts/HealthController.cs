using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {
    public float health = 100f;
    public bool isDead = false;

    void Update() {
        if(health <= 0f) {
            isDead = true;
            if(transform.CompareTag("Npc_Guard")) {
                GetComponent<NPC_Generic>().actorActions.Clear();
                GetComponent<NPC_Generic>().actorActions.Add(new Actor.NPCAction(null, 0));
                transform.GetChild(0).localRotation = new Quaternion(55.68001f, 358.83f, 279.0699f, 0f);
                transform.GetChild(0).localPosition = new Vector3(-0.14f, -0.46f, 0.24f);
            }
        } else {
            isDead = false;
        }
    }
}
