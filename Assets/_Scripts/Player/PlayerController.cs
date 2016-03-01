using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public enum CameraMode { First, Third, Arrow };
    public CameraMode currentCameraMode = CameraMode.Third;

    public GameObject FirstPerson;
    public GameObject ThirdPerson;
    public GameObject ArrowMode;

    private PlayerStats ps;

    void Awake() {
        ps = GetComponent<PlayerStats>();    
    }

    void Update() {
        if(isDead()) {
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
                Vector3 ArrowPos = new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z);
                GameObject arrow = Instantiate(ArrowMode, ArrowPos, transform.rotation) as GameObject;
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
