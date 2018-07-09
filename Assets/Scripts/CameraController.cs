using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	InputManager inputManager;
	public float rotAngel = 180.0f;
	public float horizontalAngel = 0.0f;
	public float verticalAngel = 10.0f;
	public Transform lookTarget;
	public Vector3 offset = Vector3.zero;
	public float distance = 5.0f;

	// Use this for initialization
	void Start () {
		inputManager = FindObjectOfType<InputManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (inputManager.Moved()) {
			float angelPerPixel = rotAngel / (float)Screen.width;
			Vector2 delta = inputManager.GetDeltaPosition ();
			horizontalAngel += delta.x * angelPerPixel;
			horizontalAngel = Mathf.Repeat(horizontalAngel, 360.0f);
			verticalAngel -= delta.y * angelPerPixel;
			verticalAngel = Mathf.Clamp (verticalAngel, -20.0f, 90.0f);
		}

		if (lookTarget != null) {
			Vector3 lookPosition = lookTarget.position + offset;
			Vector3 relativePos = Quaternion.Euler (verticalAngel, horizontalAngel, 0) * new Vector3 (0, 0, -distance);
			transform.position = lookPosition + relativePos;
			transform.LookAt (lookPosition);

			RaycastHit hitInfo;
			if (Physics.Linecast (lookPosition, transform.position, out hitInfo, 1 << LayerMask.NameToLayer ("Object"))) {
				transform.position = hitInfo.point;
			}
		}
	}
}