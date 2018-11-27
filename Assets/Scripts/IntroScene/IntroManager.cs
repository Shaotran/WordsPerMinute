using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {
	public int Stage = 0; //0= stationary, 1=Waiting to Say, 2=Waiting to Stop Talking

	public GameObject BG_Plain;
	public GameObject BG_Image;


	public GameObject LoadPreviousTextBtn;
	public GameObject TitleText;
	public GameObject SAYText;
	public GameObject SayContents;
	public Color TextNormalColor;
	public GameObject BottomInstructions;
	public GameObject RecordingText; //ONLY Stage 2
	public GameObject SimpleWdsButton;
	public GameObject ActualButton;
	public GameObject StartText_OfButton;
	public GameObject LoadingImage_OfButton;

	AsyncOperation operation; //Temp operation vaiable

	int Stage1Timer;
	int Stage3Timer; //Used to edit the ellipses (...)
	bool NaNError_bool = false;
	bool Scene1TransitionsDone = false; //Animations, etc. - ready for text to display


	void Start () {
		Application.targetFrameRate = 60;
		RecordingText.GetComponent<Text> ().text = "";
		BottomInstructions.GetComponent<Text> ().text = "";
	}

	void Update () {
		
		if (Stage == 1) {
			if (operation.progress > 0.85f) {
				LoadingImage_OfButton.SetActive (false);
				ActualButton.GetComponent<Animator> ().SetTrigger ("Expand");
				LoadingImage_OfButton.SetActive (false);
			}

			if (Scene1TransitionsDone == false && ActualButton.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("FinalIdle"))
			{	
				Scene1TransitionsDone = true;
				BG_Plain.SetActive (true);
				BG_Image.SetActive (false);
				SimpleWdsButton.SetActive (false);
				TitleText.SetActive (false);
			}

			if (Scene1TransitionsDone) { //Repeats when animations done
				/*
				if (BG_Mid.GetComponent<RectTransform> ().anchoredPosition.y < -9)
					BG_Mid.GetComponent<RectTransform> ().position += transform.up * Time.deltaTime * 4000;
				if (BG_Bottom.GetComponent<RectTransform> ().anchoredPosition.y < -164)
					BG_Bottom.GetComponent<RectTransform> ().position += transform.up * Time.deltaTime * 4000;
					*/
				BG_Plain.GetComponent<Animator> ().SetTrigger ("StartTransition"); //Moves blocks up
				Invoke("DisplaySayTexts", 2);
			}
		} 


		if (Stage == 2) {
			//Fade ONLY SAYText out
			SAYText.GetComponent<Text>().color -= new Color(0,0,0,0.2f);

			//Recording... Text of BottomInstructions
			Stage3Timer++;

			if (Stage3Timer > 0)
				RecordingText.GetComponent<Text> ().text = "Recording";
			if (Stage3Timer > 30)
				RecordingText.GetComponent<Text> ().text = "Recording .";
			if (Stage3Timer > 60)
				RecordingText.GetComponent<Text> ().text = "Recording . .";
			if (Stage3Timer > 90)
				RecordingText.GetComponent<Text> ().text = "Recording . . .";
			if (Stage3Timer > 120)
				Stage3Timer = 0;


		}

			
		
			

	}

	public void Clicked () {
		switch (Stage) {
		case 0: //Start Text Disappears, Button Shrinks/Rotates (Anim), Scene Loaded, Button expands, Record text appears
			StartText_OfButton.SetActive (false);
			LoadPreviousTextBtn.SetActive (false);
			ActualButton.GetComponent<Animator> ().SetTrigger ("Shrink");

			Invoke ("PreloadNextScene", 2);
			break;
		case 1:
			Stage++;
			gameObject.GetComponent<AudioToThreshold> ().Click (); //Trigger Click
			gameObject.GetComponent<AudioToThreshold> ().on = true;
			BottomInstructions.GetComponent<Text> ().text = "tap to stop";

			break;
		case 2:
			Stage++;
			gameObject.GetComponent<AudioToThreshold> ().Click (); //Trigger Click
			if (!NaNError_bool) //If no NaN Error
				BottomInstructions.GetComponent<Text> ().text = "calibrating ...";
			RecordingText.GetComponent<Text> ().text = "";
			break;
		}
	}

	public void NaNError () {
		NaNError_bool = true;
		BottomInstructions.GetComponent<Text> ().text = "Error: Please Try Again";
		Invoke ("ReloadSceneError", 1.5f);
	}

	void ReloadSceneError () { //Separate Function to Accomodate Invoke Requirements
		SceneManager.LoadScene("Home"); //Reloads Scene
	}

	public void SimpleText () {
		gameObject.GetComponent<AudioToThreshold> ().numOfActualWords = 8;
		SayContents.GetComponent<Text> ().text = "PURPLE GUINEA PIGLETS STUDY\nSPANISH EVERY MONDAY MORNING ";
	}

	IEnumerator WaitAndCallback(float waitTime){
		yield return new WaitForSeconds(waitTime);   
	}

	void PreloadNextScene () { //Created for Invoke - otherwise it lags animation
		operation = SceneManager.LoadSceneAsync ("WPM");
		operation.allowSceneActivation = false;
		Stage++;
	}

	void DisplaySayTexts () { //Created for Invoke - to appear after animation
		SAYText.SetActive (true);
		SayContents.SetActive (true);
		BottomInstructions.GetComponent<Text> ().text = "tap to record";
	}
}
