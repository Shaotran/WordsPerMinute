using UnityEngine;
using System.Collections;
//For WPM Timer
public class TimerScript : MonoBehaviour {

	public float currentTime = 1;
	public float totalTime = 0;
	public bool TimerBool = false;

	void ResetWPM () {
		TimerBool = false;
		currentTime = 1;
		totalTime = 0;
	}

	void Update () {
		if (TimerBool) {
			currentTime += Time.deltaTime;
			totalTime += Time.deltaTime;
		}

		if (currentTime > 9.95 && currentTime < 10.05) {
			gameObject.GetComponent<WordCount>().Words -= Mathf.RoundToInt(gameObject.GetComponent<WordCount>().WPM/12);
			currentTime -= 5;
		}


	}

	public void StartTimer () {
		TimerBool = true;
	}


}
