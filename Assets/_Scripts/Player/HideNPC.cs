using UnityEngine;
using System.Collections;

public class HideNPC : MonoBehaviour {
    public GameObject gmOnBack;

    public void pickUpGameObject(GameObject gm) {
        if(gm.transform.CompareTag("Npc_Guard")) {
            if(!gm.GetComponent<HealthController>().isDead)
                return;
            gm.transform.SetParent(this.transform);
            gm.transform.localPosition = new Vector3(0.38f, -0.77f, -2.41f);
            gmOnBack = gm;
        }
    }

    public void dropGameObject(GameObject gm) {
        if(gm.transform.CompareTag("Npc_Guard")) {
            gmOnBack = null;
            
            //Hopefully it works
            gm.transform.SetParent(null);
        }
    }
}
