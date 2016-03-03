using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
    public Transform player;
    [Range(0.1f, 10f)]
    public float rotateSense = 2f;
    [Range(0.1f, 10f)]
    public float mouseRotateSense = 7f;


    void Update() {
        transform.position = player.position;
        if(Input.GetMouseButton(2)) {
            transform.Rotate(new Vector3(0f, Input.GetAxis("Mouse X") * -mouseRotateSense, 0f));
        }
        transform.Rotate(new Vector3(0f, Input.GetAxis("Horizontal") * -rotateSense, 0f));
    }

}
