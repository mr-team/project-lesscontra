using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {
    private float rotationX;
    private float rotationY;
    private bool hitSomthing = false;
    private float speed = 10f;

    private GameObject blood;
    private GameObject cam;
    private PlayerController player;

    void Start () {
        blood = transform.FindChild("arrow").FindChild("Particle_Blood").gameObject;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        cam = transform.FindChild("arrow").FindChild("Camera").gameObject;
    }

    void FixedUpdate() {
        if (!hitSomthing) {
            ControlArrow();
            transform.position = transform.position + transform.forward * Time.deltaTime * speed / 2;
        }
    }

    void OnCollisionEnter(Collision other)  {
        hitSomthing = true;
        transform.parent = other.gameObject.transform;
        transform.GetComponent<BoxCollider>().enabled = false;
        Destroy(cam);
        player.currentCameraMode = PlayerController.CameraMode.Third;

        if (other.transform.tag == "Npc_Guard") {
            blood.GetComponent<ParticleSystem>().Play();
        }
    }

    void ControlArrow() {
        rotationX = transform.localEulerAngles.y + Input.GetAxis("Horizontal");
        rotationY = transform.localEulerAngles.x - Input.GetAxis("Vertical");
        Vector3 calcRotation = new Vector3(rotationY, rotationX, 0);
        transform.localEulerAngles = (calcRotation);
    }
}
