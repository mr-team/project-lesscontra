using UnityEngine;
using System.Collections;

public class ArrowModeController : MonoBehaviour {
    private float mouseRotateSense;


    void Start(){
        mouseRotateSense = GameObject.Find("Camera Focus").GetComponent<FollowPlayer>().mouseRotateSense;
    }

    void Update() {
        transform.Rotate(new Vector3(0f, Input.GetAxis("Mouse X") * mouseRotateSense, 0f));
    }
}
