using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {
    private GameObject vertical;

    void Start() {
        vertical = transform.FindChild("VerticalRotate").gameObject;
    }
	
	void Update() {
        transform.Rotate(new Vector3(0f, Input.GetAxis("Horizontal"), 0f));
        vertical.transform.Rotate(new Vector3(Input.GetAxis("Vertical"), 0f, 0f));
    }
}
