using UnityEngine;
using System.Collections;

public class ShittyMovementPrototype : MonoBehaviour {
    public Transform from;
    public Quaternion to;
    public float speed = 0.1F;

    private int counter = 499;
    private int target = 500;

    void Awake() {
        from = transform;
        to = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
    }

    void FixedUpdate() {
        counter++;
        if(counter >= target) {
            counter = Random.Range(0, target);
            from = transform;
            to = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        }
        transform.rotation = Quaternion.RotateTowards(from.rotation, to, 0.6f);
    }
}
