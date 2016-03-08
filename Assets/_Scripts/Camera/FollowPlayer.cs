using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
    public Transform player;
    [Range(0.1f, 10f)]
    public float rotateSense = 2f;
    [Range(0.1f, 10f)]
    public float mouseRotateSense = 7f;
    [Range(-5f, 3f)]
    public float cameraZoom = 0f;

    private Transform cam;

    void Awake() {
        cam = transform.FindChild("CameraControl").FindChild("Main Camera");
    }

    void Update() {
        transform.position = player.position;
        if(Input.GetMouseButton(2)) {
            transform.Rotate(new Vector3(0f, Input.GetAxis("Mouse X") * -mouseRotateSense, 0f));
        }
        transform.Rotate(new Vector3(0f, Input.GetAxis("Horizontal") * -rotateSense, 0f));

        Vector3 newPos = new Vector3(cam.localPosition.x, cam.localPosition.y, cam.localPosition.z + Input.GetAxis("Vertical") * 0.1f);
        newPos.z = Mathf.Clamp(newPos.z, -5f, 3f);
        cam.localPosition = newPos;
    }

}
