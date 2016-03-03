using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

    private float mouseRotateSense;
    private float rotationX;
    private float rotationY;
    private bool hitSomthing = false;
    private float speed = 10f;
    private PlayerController player;

    //private Rigidbody arrowRigid;

    private GameObject blood;

    void Start () {
        //arrowRigid = GetComponent<Rigidbody>();
        mouseRotateSense = GameObject.Find("Camera Focus").GetComponent<FollowPlayer>().mouseRotateSense;
        blood = transform.FindChild("arrow").transform.FindChild("Particle_Blood").gameObject;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void FixedUpdate() {
        
        ControlArrow();

        if (!hitSomthing) {
            transform.position = transform.position + transform.forward * Time.deltaTime * speed / 2;
        }

    }

    void OnCollisionEnter(Collision other)  {
        Debug.Log("KRIS IS A FUCKING FGT");
        //arrowRigid.isKinematic = true;
        hitSomthing = true;
        transform.parent = other.gameObject.transform;
        GameObject cam = transform.FindChild("arrow").transform.FindChild("Camera").gameObject;
        transform.GetComponent<BoxCollider>().enabled = false;
        Destroy(cam);
        player.currentCameraMode = PlayerController.CameraMode.Third;

        if (other.transform.tag == "Npc_Guard") {
            blood.GetComponent<ParticleSystem>().Play();
        }
    }

    void ControlArrow() {
        if(hitSomthing)
            return;
        rotationX = transform.localEulerAngles.y + Input.GetAxis("Horizontal");
        rotationY = transform.localEulerAngles.x - Input.GetAxis("Vertical");
        Vector3 calcRotation = new Vector3(rotationY, rotationX, 0);
        transform.localEulerAngles = (calcRotation);
    }
}
