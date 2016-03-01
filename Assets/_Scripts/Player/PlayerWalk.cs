using UnityEngine;
using System.Collections;

public class PlayerWalk : MonoBehaviour {
    private NavMeshAgent agent;
    public Vector3 targetPos;
    public GameObject[] feedBackElements;

    private PlayerController PC;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        PC = GetComponent<PlayerController>();
    }
    
	void Update() {
        if(Input.GetMouseButtonUp(0) && PC.currentCameraMode == PlayerController.CameraMode.Third) {
            if(CheckClickedLayer() == 8) {
                SetTargetPosition(CheckClickedLayer());
                agent.SetDestination(targetPos);
                WalkPointAnim(targetPos);
            }
        }
	}

    private void SetTargetPosition(int layer) {
        if(layer == 8) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit)) //if the raycast hit somthing
            {
                if(hit.transform.gameObject.layer == 8) //if it hit an object in the ground layer
                    targetPos = hit.point; //get the point where ray hit the object
            }
        }
    }

    public void WalkPointAnim(Vector3 Point) {
        Point = new Vector3(Point.x, Point.y + 0.03f, Point.z);
        GameObject tempElement = Instantiate(feedBackElements[0], Point, Quaternion.identity) as GameObject;
        tempElement.transform.eulerAngles = (new Vector3(90, 0, 0));
        tempElement.name = "Mouse Click Location: " + Point;
        Destroy(tempElement, 0.5f);
    }

    private int CheckClickedLayer() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit) && hit.transform != null)
            return hit.transform.gameObject.layer;

        return 9;
    }
}
