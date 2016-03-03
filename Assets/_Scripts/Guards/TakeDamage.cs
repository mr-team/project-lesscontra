using UnityEngine;
using System.Collections;

public class TakeDamage : MonoBehaviour {
    public float damageMultiplier = 1f;

    private GuardStats GS;

    void Awake() {
        GS = this.transform.parent.parent.parent.GetComponent<GuardStats>();
    }

    void OnTriggerEnter(Collider coll) {
        if(coll.gameObject.CompareTag("Weapon")) {
            float baseDamage = Random.Range(0.00f, 24.99f);
            GS.health -= baseDamage * damageMultiplier;
        }
    }
}
