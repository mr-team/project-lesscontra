using UnityEngine;
using System.Collections;

public class GuardAttacker : MonoBehaviour {

    public GameObject player;

    void Start() {
        player = GameObject.Find("Player");
    }

	void Update () {
        if((transform.position - player.transform.position).sqrMagnitude < 81) {
            if(Mathf.Abs(CalcAngle(player.transform.position - transform.position)) < 40) {
                Debug.Log("Player in sight!!!");
                player.transform.position = new Vector3(145.17f, 1f, 121.16f);
            }
        }
    }

    private float CalcAngle(Vector3 newDirection) {
        Vector3 referenceForward = transform.forward;
        Vector3 referenceRight = transform.right;
        float angle = Vector3.Angle(newDirection, referenceForward);
        float sign = Mathf.Sign(Vector3.Dot(newDirection, referenceRight));
        return sign * angle;
    }
}
