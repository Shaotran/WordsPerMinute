using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PopUpManager : MonoBehaviour {
	public GameObject Manager;
	public GameObject RealTimer;

	public GameObject AverageWPM;
	public GameObject TotalWords;
	public GameObject TimeElapsed;
	public GameObject MovingWheel;
	List <float> WPMListNew = new List <float>();


	void Start () {
		gameObject.SetActive (false);
	}

	void Pop () {
		//Average WPM:
		WPMListNew = Manager.GetComponent<WordCount> ().WPMList;

		float sum = 0;
		for (int num = 0; num < WPMListNew.Count; num++) {
			sum += WPMListNew [num];
		}
		float AvWPM = sum / WPMListNew.Count;
		AverageWPM.GetComponent<Text> ().text = Mathf.RoundToInt (AvWPM) + "";




		//Total Words Spoken:
		TotalWords.GetComponent<Text>().text = "Total Words Spoken: " + Manager.GetComponent<WordCount>().TotalWords;





		//Time Elapsed:
		int minutes = Mathf.FloorToInt (Manager.GetComponent<TimerScript>().totalTime / 60);
		int seconds = Mathf.FloorToInt (Manager.GetComponent<TimerScript>().totalTime - (minutes * 60));
		if (seconds.ToString ().Length == 1) {
			TimeElapsed.GetComponent<Text> ().text = "Time Elapsed: " + minutes + ":" + "0" + seconds;
		}
		if (seconds.ToString ().Length == 2) {
			TimeElapsed.GetComponent<Text> ().text = "Time Elapsed: " + minutes + ":" + seconds;
		}


		//Moving Wheel:
		MovingWheel.GetComponent<Image>().fillAmount = AvWPM / Manager.GetComponent<WordCount>().IntendedWPM;
	}

	public void ExitPop () {
		Manager.SendMessage ("ResetWPM");
		RealTimer.SendMessage ("ResetWPM");
		gameObject.SetActive (false);
	}
}
