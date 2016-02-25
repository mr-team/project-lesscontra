using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
    public Transform player;

    private Vector3 cameraOffset = new Vector3(-20.09947f, 22.03662f, -19.65461f);

    void Update() {
        transform.position = player.position + cameraOffset;
    }

}
