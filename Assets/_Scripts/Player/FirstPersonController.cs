using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {
    private GameObject player;
    private Transform VerticalAnchor;

    void Start () {
        player = GameObject.Find("Player");
        VerticalAnchor = transform.FindChild("VerticalAnchor");
    }
	
	void Update() {
        player.transform.Rotate(new Vector3(0f, Input.GetAxis("Horizontal"), 0f));
        VerticalAnchor.Rotate(new Vector3(Input.GetAxis("Vertical"), 0f, 0f));
    }
}
