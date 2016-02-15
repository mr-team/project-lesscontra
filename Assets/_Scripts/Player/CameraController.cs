using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Vector3 planarRotationY;
	public Vector3 planarRotationX;

	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;
	float rotationY = 0F;
	float rotationX = 0f;

	void Start ()
	{
	
	}

	
	void Update ()
	{
		CamControll ();
	}

	void CamControll ()
	{
		rotationX = transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityX;

		planarRotationY = new Vector3 (0, rotationX, 0);

		if (rotationX > 180)
		{
			rotationX -= 360;
		}

		if (rotationX <= minimumX)
			rotationX = minimumX;

		if (rotationX >= maximumX)
			rotationX = maximumX;

		rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

		Vector3 calcRotation = new Vector3 (-rotationY, rotationX, 0);
		planarRotationX = new Vector3 (-rotationY, 0, 0);

		transform.localEulerAngles = calcRotation;

	}
}
