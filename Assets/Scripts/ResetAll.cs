using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAll : MonoBehaviour {

	void Start () {
		PlayerPrefs.SetInt ("New", 0);
		PlayerPrefs.SetFloat ("Threshold", 0);
		PlayerPrefs.SetInt ("DefaultTime", 0);
	}

}
