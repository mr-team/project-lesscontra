using UnityEngine;
using System.Collections;

public class GuardCommunicator : MonoBehaviour {
    public float timeToForgetLastKnownLocation = 25f;
    
    public Vector3 lastKnowLocation = new Vector3(0f, -1337f, 0f);
    public float forgetLocationTimer = 0f;
    public float alarmRadius = 15f;
    
	void FixedUpdate() {
        if(hasLastKnownLocation()) {
            forgetLocationTimer += Time.fixedDeltaTime;
            if(forgetLocationTimer >= timeToForgetLastKnownLocation) {
                lastKnowLocation = new Vector3(0f, -1337f, 0f);
                forgetLocationTimer = 0f;
            }
        }
	}

    public bool hasLastKnownLocation() {
        return lastKnowLocation != new Vector3(0f, -1337f, 0f);
    }

    public Vector3 getLastKnownLocation() {
        return lastKnowLocation;
    }

    public void setLastKnownLocation(Vector3 loc) {
        lastKnowLocation = loc;
        forgetLocationTimer = 0f;
    }

}
