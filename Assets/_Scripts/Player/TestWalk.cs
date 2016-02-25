using UnityEngine;
using System.Collections;

public class TestWalk : MonoBehaviour {
    private NavMeshAgent agent;
    public Vector3 targetPos;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }
    
	void Update() {
        if(Input.GetMouseButtonUp(0)) {
            if(CheckClickedLayer() == 8) {
                SetTargetPosition(CheckClickedLayer());
                agent.SetDestination(targetPos);
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

    private int CheckClickedLayer() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit) && hit.transform != null)
            return hit.transform.gameObject.layer;

        return 9;
    }
}
