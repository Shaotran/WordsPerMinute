using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Given 8-band audio from AudioAnalyzer, creates Arraylist of avg. band frequencies

public class AudioToThreshold : MonoBehaviour {
	public bool on = false;

	float[] FrameEightBands = new float[8];
	public float sum; //temp sum for calculating average

	List<float> TotalFrequencies = new List<float>(); //Contains Y graph of all frequencies (averages) from each Update frame
	public List<float> WorkingThresholds = new List<float>(); //Arraylist of all Working Thresholds
	bool prevAboveThreshold = false;

	public int numOfActualWords;

	void Start () {
		TotalFrequencies = new List<float> ();
		WorkingThresholds = new List<float> ();
	}

	void Update () {
		if (on == true) {
			sum = 0;
			FrameEightBands = AudioAnalyzer.freqBands;
			foreach (float x in FrameEightBands) 
			{
				sum += x;
			}
			TotalFrequencies.Add (sum / 8); 
		}

	}

	public void Click () {
		if (on == false)
			on = true;
		else if (on == true) {
			on = false;
			calcThreshold ();
		}
	}

	public void calcThreshold () {
		for (float Threshold = 0; Threshold < calcMax (TotalFrequencies); Threshold = Threshold + 0.005f) { //0 to Maximum Value of TotalFrequencies -- FOR EACH Y
			//for each xVal => Find "num words"
			int CalculatedWordsForThreshold = 0;

			for (int x = 0; x < TotalFrequencies.Count; x++) {

				/*
				if ((TotalFrequencies [x] > Threshold) && !prevAboveThreshold) { //Above
					CalculatedWordsForThreshold++;
					prevAboveThreshold = true;

				} else { //Below
					prevAboveThreshold = false;
				}*/

				//CROSS THEORY
				if ((TotalFrequencies [x] <= Threshold) && prevAboveThreshold) { //Above
					CalculatedWordsForThreshold++;
					prevAboveThreshold = false;
				} else if (TotalFrequencies [x] > Threshold)
					prevAboveThreshold = true;

			}

			Debug.Log (CalculatedWordsForThreshold);

			if (numOfActualWords == CalculatedWordsForThreshold) // || (numOfActualWords - 1) == CalculatedWordsForThreshold || (numOfActualWords + 1) == CalculatedWordsForThreshold)
				WorkingThresholds.Add (Threshold);

		}
			

		float newThreshold = calcMedian (WorkingThresholds);



		PlayerPrefs.SetFloat("Threshold", newThreshold);
		Debug.Log ("Threshold: " + PlayerPrefs.GetFloat ("Threshold"));

		if (newThreshold > 0) {
			SceneManager.LoadScene ("WPM");
		}

		if (float.IsNaN (newThreshold)) {
			gameObject.GetComponent<IntroManager> ().NaNError (); //Set Text to Alert User
		}
	}



	float calcMax (List<float> arr) { //Find the max value of an arraylist
		float max = 0;
		for (int i = 0; i < arr.Count; i++) {
			if (arr[i] > max) 
				max = arr[i];
		}
		return max;
	}

	float calcMedian (List<float> arr) { //Find the median of an arraylist
		if (arr.Count % 2 == 0) { //EVEN - NUM BEFORE MIDDLE 2 4 6 8 8 9
			return (arr[arr.Count/2 - 1] );
		} else { //ODD - EXACT MIDDLE: 2 4 6 8 9
			return ( arr[ (int) (arr.Count/2 + 0.5f) ] );
		}
	}

}
