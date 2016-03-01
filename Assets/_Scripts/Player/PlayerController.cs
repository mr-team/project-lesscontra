using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    private PlayerStats ps;

    void Awake() {
        ps = GetComponent<PlayerStats>();    
    }

    void Update() {
        if(isDead()) {
            Debug.Log("Player died!");
        }
    }

    public void takeDamage(float damage) {
        ps.health -= Mathf.Clamp(damage, 0f, ps.health);
    }

    public bool isDead() {
        return ps.health <= 0f;
    }
}
