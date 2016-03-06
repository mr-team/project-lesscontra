using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public enum CameraMode { First, Third, Arrow };
    public CameraMode currentCameraMode = CameraMode.Third;

    public GameObject FirstPerson;
    public GameObject ThirdPerson;
    public GameObject ArrowMode;

    private PlayerStats ps;
    private GameObject arrowSpawn;
    private Animation archerAni;

    void Awake() {
        ps = GetComponent<PlayerStats>();
        arrowSpawn = FirstPerson.transform.FindChild("VerticalAnchor").FindChild("arm_with_bow").FindChild("ArrowSpawn").gameObject;
        archerAni = ThirdPerson.transform.FindChild("Archer").GetComponent<Animation>();
    }

    void Update() {
        if(isDead()) {
            archerAni.clip = archerAni.GetClip("Death");
            archerAni.Play();
            Debug.Log("Player died!");
        }
        if(currentCameraMode == CameraMode.Third) {
            if (FirstPerson.activeInHierarchy)
                FirstPerson.SetActive(false);
            if (!ThirdPerson.activeInHierarchy)
                ThirdPerson.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            if (Input.GetKeyDown(KeyCode.Space)) {
                currentCameraMode = CameraMode.First;
            }
        } else if(currentCameraMode == CameraMode.First) {
            if (!FirstPerson.activeInHierarchy)
                FirstPerson.SetActive(true);
            if (ThirdPerson.activeInHierarchy)
                ThirdPerson.SetActive(false);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (Input.GetKeyDown(KeyCode.Space)) {
                currentCameraMode = CameraMode.Third;
            }
            if (Input.GetMouseButtonDown(0)) {
                currentCameraMode = CameraMode.Arrow;
                FirstPerson.SetActive(false);
                Transform vert = FirstPerson.transform.FindChild("VerticalAnchor");
                GameObject arrow = Instantiate(ArrowMode, arrowSpawn.transform.position, arrowSpawn.transform.rotation) as GameObject;
            }
        } else {
            if (FirstPerson.activeInHierarchy)
                FirstPerson.SetActive(false);
            if (ThirdPerson.activeInHierarchy)
                ThirdPerson.SetActive(false);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void takeDamage(float damage) {
        ps.health -= Mathf.Clamp(damage, 0f, ps.health);
    }

    public bool isDead() {
        return ps.health <= 0f;
    }
}
