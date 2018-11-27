using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//For Normal Timer
//Also controls Handheld Vibrate

public class TimerManager : MonoBehaviour {

	public int TotalSeconds;
	public float CurrentSeconds;
	public bool TimerBool = false; //Start timer?
	public bool Infinite = false;

	//Displayed Time:
	int minutes;
	int seconds;

	public GameObject RadialTimer;
	public Color NormalGreen;

	//Ending Animation Flash Timer:
	public int AnimCounter = 0;

	void Start () {
		Time.timeScale = 1;
		TimerBool = false;
		CurrentSeconds = TotalSeconds;
		DisplayTime ();
	}

	void ResetWPM () {
		Time.timeScale = 1;
		TimerBool = false;
		AnimCounter = 0;
		RadialTimer.GetComponent<Image> ().color = NormalGreen;
		RadialTimer.GetComponent<Image> ().fillAmount = 1;
		gameObject.GetComponent<Text> ().color = Color.white;
		CurrentSeconds = TotalSeconds;
		DisplayTime ();
	}

	void TimerStart () {
		if (TimerBool == false && Infinite == false) {
			TimerBool = true;
			RadialTimer.GetComponent<Image> ().color = NormalGreen;
			RadialTimer.GetComponent<Image> ().fillAmount = 1;
			gameObject.GetComponent<Text> ().color = Color.white;
			CurrentSeconds = TotalSeconds;
		}
	}

	public void TimerSet (int timesecs) {
		TotalSeconds = timesecs;
		CurrentSeconds = timesecs;
		DisplayTime ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (TimerBool) {
			//Lower Time
			if (CurrentSeconds > 0) {
				CurrentSeconds -= Time.deltaTime;

				//Set Display Time:
				minutes = Mathf.FloorToInt (CurrentSeconds / 60);
				seconds = Mathf.FloorToInt (CurrentSeconds - (minutes * 60));

				//Display Time:
				if (seconds.ToString ().Length == 1) {
					gameObject.GetComponent<Text> ().text = minutes + ":" + "0" + seconds;
				}
				if (seconds.ToString ().Length == 2) {
					gameObject.GetComponent<Text> ().text = minutes + ":" + seconds;
				}

				//Display Radial:
				RadialTimer.GetComponent<Image> ().fillAmount = 1 - (1f * (1 - (CurrentSeconds / TotalSeconds))); //Usually 0.5f

			} //End of Positive Count

			if (Mathf.Round (CurrentSeconds) == 0) {  //If 0 Count Down
				Debug.Log("VIBRATE");
				CurrentSeconds -= Time.deltaTime;
				Handheld.Vibrate (); //iOS DEVICE VIBRATES
			}

			if (CurrentSeconds < 0) { //If Negative, Count UP, add negative to display
				CurrentSeconds -= Time.deltaTime;

				minutes = Mathf.FloorToInt ((-1 * CurrentSeconds) / 60);
				seconds = Mathf.FloorToInt ((-1 * CurrentSeconds) - (minutes * 60));


				//Display Time:
				if (seconds.ToString ().Length == 1) {
					gameObject.GetComponent<Text> ().text = "-" + minutes + ":" + "0" + seconds;
				}
				if (seconds.ToString ().Length == 2) {
					gameObject.GetComponent<Text> ().text = "-" + minutes + ":" + seconds;
				}


				//Display Radial:
				RadialTimer.GetComponent<Image> ().color = Color.red;
				RadialTimer.GetComponent<Image> ().fillAmount = 1;
				gameObject.GetComponent<Text> ().color = Color.red;
					
				//Timer System:
				AnimCounter ++ ;
				if (AnimCounter == 20) {
					AnimCounter = 0;

					if (RadialTimer.GetComponent<Image> ().enabled == true) {
						RadialTimer.GetComponent<Image> ().enabled = false;
					}
					else if (RadialTimer.GetComponent<Image> ().enabled == false)
						RadialTimer.GetComponent<Image> ().enabled = true;
				}
					
			} //End of Negative Count








		} //End of First Conditional


	} //End of Update

	void DisplayTime () {
		minutes = Mathf.FloorToInt (TotalSeconds / 60);
		seconds = Mathf.FloorToInt (TotalSeconds - (minutes * 60));
		if (Infinite == false) { //Only if not Infinite
			if (seconds.ToString ().Length == 1) {
				gameObject.GetComponent<Text> ().text = minutes + ":" + "0" + seconds;
			}
			if (seconds.ToString ().Length == 2) {
				gameObject.GetComponent<Text> ().text = minutes + ":" + seconds;
			}
		}
	}

	public void InfiniteTime () {
		Infinite = true;
		TimerBool = false;
		gameObject.GetComponent<Text> ().text = "";
	}

}
