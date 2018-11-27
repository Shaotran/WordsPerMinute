using UnityEngine;
using System.Collections;

public class CameraScale: MonoBehaviour {

	void Awake () {
		gameObject.GetComponent<Camera>().orthographicSize = Screen.height / 2;
	}
}
