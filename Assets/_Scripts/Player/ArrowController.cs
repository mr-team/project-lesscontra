using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {
    public float minimumX;
    public float maximumX;
    public float minimumY;
    public float maximumY;

    private float mouseRotateSense;
    private float rotationX;
    private float rotationY;
    private bool arrowFired = true;
    private bool hitSomthing = false;
    private float speed = 10f;

    //private Rigidbody arrowRigid;

    private GameObject blood;

    void Start () {
        //arrowRigid = GetComponent<Rigidbody>();
        mouseRotateSense = GameObject.Find("Camera Focus").GetComponent<FollowPlayer>().mouseRotateSense;
        blood = transform.FindChild("Particle_Blood").gameObject;
    }

    void FixedUpdate() {
        /*if (arrowFired && physicsMode)
		{
			transform.Translate (Vector3.forward.x * speed * Time.deltaTime, 0, Vector3.forward.z * speed * Time.deltaTime);
			ControlArrowXZ ();
		}*/

        if (arrowFired) {
            ControlArrow();
        }

        if (arrowFired && !hitSomthing) {
            transform.position = transform.position + transform.up * Time.deltaTime * speed / 2;
        }

    }

    void OnCollisionEnter(Collision other)  {
        //arrowRigid.isKinematic = true;
        arrowFired = false;
        hitSomthing = true;
        transform.parent = other.gameObject.transform;
        Transform cam = transform.FindChild("Camera");
        cam.gameObject.SetActive(false);
        
        if (other.transform.tag == "Npc_Guard") {
            blood.GetComponent<ParticleSystem>().loop = true;
        }
    }

    void ControlArrow() {
        //offset = 45;

        rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseRotateSense;

        if (rotationX > 180) {
            rotationX -= 360;
        }

        if (rotationX <= minimumX)
            rotationX = minimumX;

        if (rotationX >= maximumX)
            rotationX = maximumX;

        rotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * mouseRotateSense;

        /*if (rotationY > 180)
		{
			rotationY -= 360;
		}
		if (rotationY <= minimumY)
			rotationY = minimumY;
		if (rotationY >= maximumY)
			rotationY = maximumY;*/

        //rotationY += rotationX + Input.GetAxis ("Mouse Y") * sensitivityY;
        //rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

        //Debug.Log(Input.GetAxis("Mouse Y"));

        Vector3 calcRotation = new Vector3(rotationY, rotationX, 0);

        transform.localEulerAngles = (calcRotation);
    }
}
