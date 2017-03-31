using UnityEngine;
using System.Collections;

public class MouseRotate : MonoBehaviour {

	[Range(50f,500f)]
	public float sensitivity = 100f;

	public static bool cursorIsLocked
	{
		get
		{
			return !Cursor.visible;
		}
		set
		{
			Cursor.visible = !value;
			if (value) Cursor.lockState = CursorLockMode.Locked;
			else Cursor.lockState = CursorLockMode.None;
		}
	}

	// Use this for initialization
	void Start () {
		cursorIsLocked = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown("Fire1"))
		{
			cursorIsLocked = true;
		}

		if (Input.GetKeyDown("escape"))
		{
			cursorIsLocked = false;
		}

		if (cursorIsLocked) {

			Vector3 delta = new Vector3 (Input.GetAxis ("Mouse Y"), -Input.GetAxis ("Mouse X"), 0) * Time.deltaTime * sensitivity;
			transform.Rotate (delta, Space.World);
		}
	}
}
