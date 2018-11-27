using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadPrevData : MonoBehaviour {

	public void Clicked () {
		if (PlayerPrefs.GetFloat ("Threshold") != 0) {
			gameObject.GetComponent<Text> ().text = "Loading...";
			SceneManager.LoadScene ("WPM");
		} else {
			gameObject.GetComponent<Text> ().text = "Locked For New Users";
		}
	}
}
