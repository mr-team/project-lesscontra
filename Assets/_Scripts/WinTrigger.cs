using UnityEngine;
using System.Collections;

public class WinTrigger : MonoBehaviour
{


	void Start ()
	{
	
	}


	void Update ()
	{
	
	}

	void OnTriggerEnter (Collider other)
	{
		Debug.Log ("was hit");
		if (other.tag == "Arrow")
		{
			Debug.Log ("Was hit by arrow");
			Destroy (other.gameObject);
		}
	}

}
