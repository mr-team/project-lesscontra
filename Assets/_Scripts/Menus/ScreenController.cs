using UnityEngine;
using System.Collections;

public class ScreenController : MonoBehaviour {
    public PlayerStats ps;
    public RectTransform LoseScreen;
    public float deathScreenCooldown = .7f;

    private float deathScreenTimer = 0f;

    void Update() {
        if(ps.health <= 0f) {
            deathScreenTimer += Time.deltaTime;
            if(deathScreenTimer >= deathScreenCooldown) {
                LoseScreen.gameObject.SetActive(true);
            }
        }
    }
}
