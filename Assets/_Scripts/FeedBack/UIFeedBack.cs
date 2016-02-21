using UnityEngine;
using System.Collections;

public class UIFeedBack : MonoBehaviour
{
	
	public GameObject[] feedBackElements;

	void Start ()
	{
		
	}

	public void WalkPointAnim (Vector3 Point)
	{
		GameObject tempElement = Instantiate (feedBackElements [0], Point, Quaternion.identity) as GameObject;
		tempElement.transform.eulerAngles = (new Vector3 (90, 0, 0));
		Destroy (tempElement, 0.5f);
	}
}
