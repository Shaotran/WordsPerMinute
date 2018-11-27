using UnityEngine;
using System.Collections;

public class StartStopUI : MonoBehaviour {
	public GameObject PopUpWindow;
	public GameObject WPMText; //Manager of WPM
	public GameObject TimerReal;

	public void WPMTimerClicked () {
		if (WPMText.GetComponent<WordCount> ().Words != 0) {
			PopUpWindow.SetActive (true);
			PopUpWindow.SendMessage ("Pop");
			Time.timeScale = 0;
		}
	}

	public void RealTimerClicked () {
		TimerReal.SendMessage ("TimerStart");
	}
}
