using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Transform FPModeAnchor;
	public Transform FPModePlane;

	PlayerController playerControll;

	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;
	float rotationY = 0F;
	float rotationX = 0f;

	void Awake ()
	{
		playerControll = GetComponent<PlayerController> ();

	}

	void Update ()
	{
		if (playerControll.FPModeActive)
		{
			RotateAnchor ();
			RotateInPlane ();
		} else
			ResetRotation ();
	}

	void RotateAnchor ()
	{
		rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

		Vector3 calcRotation = new Vector3 (-rotationY, 0, 0);

		FPModeAnchor.localEulerAngles = calcRotation;

	}

	void RotateInPlane ()
	{
		rotationX = FPModePlane.localEulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityX;

		if (rotationX > 180)
		{
			rotationX -= 360;
		}

		if (rotationX <= minimumX)
			rotationX = minimumX;

		if (rotationX >= maximumX)
			rotationX = maximumX;

		Vector3 calcRotation = new Vector3 (0, rotationX, 0);

		FPModePlane.localEulerAngles = calcRotation;

	}

	void ResetRotation ()
	{
		rotationY = 0f;
		FPModeAnchor.localEulerAngles = new Vector3 (0, 0, 0);
		FPModePlane.localEulerAngles = new Vector3 (0, 0, 0);
	}
}