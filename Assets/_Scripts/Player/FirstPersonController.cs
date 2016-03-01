using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {
    private GameObject player;

    private float mouseRotateSense;


    void Start () {
        player = GameObject.Find("Player");
        mouseRotateSense = GameObject.Find("Camera Focus").GetComponent<FollowPlayer>().mouseRotateSense;
    }
	
	void Update() {
        player.transform.Rotate(new Vector3(0f, Input.GetAxis("Mouse X") * mouseRotateSense, 0f));
    }
}
