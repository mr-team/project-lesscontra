using UnityEngine;
using System.Collections;

public class GuardController : MonoBehaviour {
    public GuardCommunicator GC;

    void FixedUpdate() {
        if(GC.hasLastKnownLocation()) {
            
        }
	}
}
