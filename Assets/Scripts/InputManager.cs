using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	Vector2 slideStartPosition;
	Vector2 prevPosition;
	Vector2 delta = Vector2.zero;	// 1フレームのマウスの移動量
	bool moved = false;

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {	
			slideStartPosition = GetCursorPosition ();
		}

		if (Input.GetButton ("Fire1")) {
			if (Vector2.Distance (slideStartPosition, GetCursorPosition ()) >= (Screen.width * 0.1f)) {
				moved = true;
			}
		}

		if (!Input.GetButtonUp ("Fire1") && !Input.GetButton("Fire1")) {
			moved = false;
		}

		if (moved) {
			delta = GetCursorPosition () - prevPosition;
		}else{
			delta = Vector2.zero;		
		}

		prevPosition = GetCursorPosition ();
	}

	public Vector2 GetCursorPosition()
	{
		return Input.mousePosition;
	}

	public Vector2 GetDeltaPosition(){
		return delta;
	}

	public bool Moved(){
		return moved;
	}
}