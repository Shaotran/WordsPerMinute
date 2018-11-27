using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;


public class WordCount : MonoBehaviour {

	public int Words;
	public int TotalWords;

	public static float[] Bands = new float[8];
	public int scaleMultiplier;
	public float averageBand;

	public List<float> VolumeBands = new List<float>();
	List<float> Averages = new List<float>(); //Not used
	public List<float> WPMList = new List<float>();

	public GameObject CircleRadial;
	public GameObject TimerReal;
	public GameObject SetText; //Appears/disappears depending on if app is analyzing speech
	//public GameObject TotalWordsText;
	public float WPM;


	public int IntendedWPM;
	public float ColorChangeSpeed;
	public float Barrier;  //Between ambient noise and human speech (in dB)
	public int BufferTime; //Amount of seconds before display starts to display; Needs time to adjust from thousands to hundreds WPM

	//Starter Bool:
	public bool Up = false; //True or false? True means number is rising

	//Wheel Color:
	public Color WheelColor;
	public Color SecondLerpColor;

	public Color DangerRed;
	public Color DangerYellow;
	public Color GoodGreen;

	//Graph:
	public GameObject Cube;
	public bool graphOn;

	void Start () {
		gameObject.GetComponent<Text> ().text = "Start";
		Barrier = PlayerPrefs.GetFloat ("Threshold");

		//GRAPH:
		if (graphOn) {
			GameObject clone = (GameObject)Instantiate (Cube, new Vector3 (0, Barrier, 100), Quaternion.identity);
			clone.transform.localScale = new Vector3 (300, 0.5f, 1);
		}
	}

	// Use this for initialization
	void ResetWPM () {
		gameObject.GetComponent<Text> ().text = "Start";
		Words = 0;
		TotalWords = 0;
		WPM = 0;
		averageBand = 0;
		Bands = new float[8];
		VolumeBands = new List<float>();
		Averages = new List<float>();
		WPMList = new List<float> ();

		SetText.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {

		Bands = AudioAnalyzer.freqBands;

		/*
		//Copy data over from AudioAnalyzer
		for (int num = 0; num < 8; num++) {
			Bands [num] = AudioAnalyzer.freqBands [num]; // * scaleMultiplier
		} */

		averageBand = (Bands [0] + Bands [1] + Bands [2] + Bands [3] + Bands [4] + Bands [5] + Bands [6] + Bands [7])/8;
		VolumeBands.Add (averageBand);


		//GRAPH:
		if (graphOn) {
			GameObject clone = (GameObject)Instantiate (Cube, new Vector3 (gameObject.GetComponent<TimerScript> ().totalTime * 10, 0.5f * (averageBand * 100), 100), Quaternion.identity);
			clone.transform.localScale = new Vector3 (0.5f, averageBand * 100, 1);
		}


		/*
		//Calculate average medium:
		float sum = 0;
		for (int num = 0; num < VolumeBands.Count; num++) {
			sum += VolumeBands [num];
		}
		float CurrentAverage = sum / VolumeBands.Count;
		Averages.Add (CurrentAverage);

		*/






		//Word Ended: (Old num above average, current num below average)
		/*
		if ( (VolumeBands [VolumeBands.Count - 2] > Averages [Averages.Count - 2]) && (averageBand < CurrentAverage) ) {
			if (Words == 0) {
				gameObject.SendMessage ("StartTimer");
			}
			Words++;
			TotalWords++;
		}
		*/
		if ( (VolumeBands [VolumeBands.Count - 2] > Barrier) && (averageBand < Barrier) ) {
			if (Words == 0) {
				gameObject.SendMessage ("StartTimer");
				TimerReal.SendMessage ("TimerStart");
				gameObject.GetComponent<Text> ().color = Color.white;
				CircleRadial.GetComponent<Image> ().fillClockwise = true;
			}
			Words++;
			TotalWords++;
		}
			
		//Avg syllables per word: 1.24f
		//Display WPM:
		WPM = ((Words)/gameObject.GetComponent<TimerScript>().currentTime) * 60; //(Words/2f)

		if (gameObject.GetComponent<TimerScript> ().totalTime > BufferTime) {
			gameObject.GetComponent<Text> ().text = Mathf.RoundToInt (WPM) + "";
		} else if (Words > 0){
			gameObject.GetComponent<Text> ().text = ". . .";
		}

		//Add WPM to List of WPM's:
		if (gameObject.GetComponent<TimerScript>().totalTime > BufferTime) {
			WPMList.Add (WPM);
		}

		//Display Total Words:
		//TotalWordsText.GetComponent<Text>().text = Mathf.RoundToInt(TotalWords / 1.24f) + "";

		//Rotate circle: (180 = Average)

		if (WPM <= IntendedWPM && gameObject.GetComponent<TimerScript> ().totalTime > BufferTime) {
			CircleRadial.GetComponent<Image> ().fillAmount = WPM / IntendedWPM;
		} else if (gameObject.GetComponent<TimerScript> ().totalTime > BufferTime) { //Above 160
			CircleRadial.GetComponent<Image> ().fillAmount = 1;
		} else if (Words != 0){ //Less than BufferTime (3?)
			//CircleRadial.GetComponent<Image> ().fillAmount = 0;
		}

		if (Words > 0) { //If analyzation starts...
			CircleRadial.GetComponent<Image> ().fillClockwise = true;
			SetText.SetActive (false);
		}

		//Circle color: (180 = Average)
		if (WPM <= 80) {
			CircleRadial.GetComponent<Image> ().color = DangerRed;
		}
		if (WPM > 80 && WPM <= 140) {
			CircleRadial.GetComponent<Image> ().color = DangerYellow;
		}
		if (WPM > 140 && WPM <= IntendedWPM) {
			CircleRadial.GetComponent<Image> ().color = GoodGreen;
		}
		if (WPM > IntendedWPM) {
			CircleRadial.GetComponent<Image> ().color = DangerRed;
		}

		//Start text/colors:
		if (Words == 0 || (gameObject.GetComponent<TimerScript> ().totalTime <= BufferTime && Words > 0)) {
			CircleRadial.GetComponent<Image>().color = Color.Lerp(SecondLerpColor, WheelColor, Mathf.PingPong(Time.time * ColorChangeSpeed, 1f));
			gameObject.GetComponent<Text>().color = Color.Lerp(Color.white, Color.yellow, Mathf.PingPong(Time.time * ColorChangeSpeed, 1f));

			//Circle Animation:

			if (CircleRadial.GetComponent<Image> ().fillAmount == 1) {
				CircleRadial.GetComponent<Image> ().fillClockwise = true;
				Up = false;

			} else if (CircleRadial.GetComponent<Image> ().fillAmount == 0) {
				CircleRadial.GetComponent<Image> ().fillClockwise = false;
				Up = true;
			} 

			if (Up == false) {
				
				CircleRadial.GetComponent<Image> ().fillAmount -= 0.02f;

			} 
			else if (Up == true) {
				CircleRadial.GetComponent<Image> ().fillAmount += 0.02f;
			} 



		}


	}
		


}
