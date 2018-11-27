using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetTimeManager: MonoBehaviour {

	public GameObject ChooseTimeFrame; //"PanelStuff"
	//public GameObject VisualFrame; //"Frame"
	public GameObject RealTimer; //"TimerText"
	public GameObject TotalTime; //"TimerText_TotalTime
	public GameObject SetTimeText; //Appear/Disappears. WordCount also controls this.

	public GameObject Button1;
	public GameObject Button2;
	public GameObject Button3;
	public GameObject Button4;
	public GameObject Button5;
	public GameObject Button6;
	public GameObject Button7;
	public GameObject Button8;
	public GameObject ButtonInfinity;

	//Original X Positions
	float Button1X;
	float Button2X;
	float Button3X;
	float Button4X;
	float Button5X;
	float Button6X;
	float Button7X;
	float Button8X;
	float ButtonInfinityX;

	bool MoveChooseTimesOn = false;
	bool TurnMoveChooseTimesOff = false;

	void Start () {
		if (PlayerPrefs.GetInt ("DefaultTime") == 0) //New Players Start at 0, set at 8 for Default
			PlayerPrefs.SetInt ("DefaultTime", 8);
		else if (PlayerPrefs.GetInt ("DefaultTime") == 10) //Returning Players Set to Infinite?
			InfiniteTime ();
		else //Returning Plyaers Set to Minutes 1 to 8 ?
			TimeChosen (PlayerPrefs.GetInt ("DefaultTime"));
		
		ChooseTimeFrame.SetActive (false); 
		MoveChooseTimesOn = false;
		TurnMoveChooseTimesOff = false;

		Button1X = Button1.transform.position.x;
		Button2X = Button2.transform.position.x;
		Button3X = Button3.transform.position.x;
		Button4X = Button4.transform.position.x;
		Button5X = Button5.transform.position.x;
		Button6X = Button6.transform.position.x;
		Button7X = Button7.transform.position.x;
		Button8X = Button8.transform.position.x;
		ButtonInfinityX = ButtonInfinity.transform.position.x;
	}

	void Update () {
		if (MoveChooseTimesOn) { //Turn On
			if (Button1.transform.position.x < Button1X - (Screen.width/10)) { //If still to left of "MidScreen", Subtracted amt=offset
				
				Button1.transform.Translate (Vector2.right * Time.deltaTime * 1500f);
				Button2.transform.Translate (Vector2.right * Time.deltaTime * 1500f);
				Button3.transform.Translate (Vector2.right * Time.deltaTime * 1500f);
				Button4.transform.Translate (Vector2.right * Time.deltaTime * 1500f);
				Button5.transform.Translate (Vector2.right * Time.deltaTime * 1500f);
				Button6.transform.Translate (Vector2.right * Time.deltaTime * 1500f);
				Button7.transform.Translate (Vector2.right * Time.deltaTime * 1500f);
				Button8.transform.Translate (Vector2.right * Time.deltaTime * 1500f); 
				ButtonInfinity.transform.Translate (Vector2.right * Time.deltaTime * 1500f); 
			} else {
				Button1.transform.position = new Vector3 (Button1X, Button1.transform.position.y, 0);
				Button2.transform.position = new Vector3 (Button2X, Button2.transform.position.y, 0);
				Button3.transform.position = new Vector3 (Button3X, Button3.transform.position.y, 0);
				Button4.transform.position = new Vector3 (Button4X, Button4.transform.position.y, 0);
				Button5.transform.position = new Vector3 (Button5X, Button5.transform.position.y, 0);
				Button6.transform.position = new Vector3 (Button6X, Button6.transform.position.y, 0);
				Button7.transform.position = new Vector3 (Button7X, Button7.transform.position.y, 0);
				Button8.transform.position = new Vector3 (Button8X, Button8.transform.position.y, 0); 
				ButtonInfinity.transform.position = new Vector3 (ButtonInfinityX, ButtonInfinity.transform.position.y, 0); 
			}
		}
		if (TurnMoveChooseTimesOff) {
			if (Button1.transform.position.x < Button1X + Screen.width) { //If still to left of "RightScreen"
				
				Button1.transform.Translate (Vector2.right * Time.deltaTime * 1500f);
				Button2.transform.Translate (Vector2.right * Time.deltaTime * 1500f);
				Button3.transform.Translate (Vector2.right * Time.deltaTime * 1500f);
				Button4.transform.Translate (Vector2.right * Time.deltaTime * 1500f);
				Button5.transform.Translate (Vector2.right * Time.deltaTime * 1500f);
				Button6.transform.Translate (Vector2.right * Time.deltaTime * 1500f);
				Button7.transform.Translate (Vector2.right * Time.deltaTime * 1500f);
				Button8.transform.Translate (Vector2.right * Time.deltaTime * 1500f);
				ButtonInfinity.transform.Translate (Vector2.right * Time.deltaTime * 1500f);
			} else {
				ChooseTimeFrame.SetActive (false); 
				TurnMoveChooseTimesOff = false;
				Button1.transform.position = new Vector3 (Button1X, Button1.transform.position.y, 0);
				Button2.transform.position = new Vector3 (Button2X, Button2.transform.position.y, 0);
				Button3.transform.position = new Vector3 (Button3X, Button3.transform.position.y, 0);
				Button4.transform.position = new Vector3 (Button4X, Button4.transform.position.y, 0);
				Button5.transform.position = new Vector3 (Button5X, Button5.transform.position.y, 0);
				Button6.transform.position = new Vector3 (Button6X, Button6.transform.position.y, 0);
				Button7.transform.position = new Vector3 (Button7X, Button7.transform.position.y, 0);
				Button8.transform.position = new Vector3 (Button8X, Button8.transform.position.y, 0); 
				ButtonInfinity.transform.position = new Vector3 (ButtonInfinityX, ButtonInfinity.transform.position.y, 0); 
			}
		}
	}

	public void StartPress () {
		ChooseTimeFrame.SetActive (true);
		Button1.transform.position = new Vector3 (Button1.transform.position.x - Screen.width, Button1.transform.position.y, 0);
		Button2.transform.position = new Vector3 (Button2.transform.position.x - Screen.width, Button2.transform.position.y, 0);
		Button3.transform.position = new Vector3 (Button3.transform.position.x - Screen.width, Button3.transform.position.y, 0);
		Button4.transform.position = new Vector3 (Button4.transform.position.x - Screen.width, Button4.transform.position.y, 0);
		Button5.transform.position = new Vector3 (Button5.transform.position.x - Screen.width, Button5.transform.position.y, 0);
		Button6.transform.position = new Vector3 (Button6.transform.position.x - Screen.width, Button6.transform.position.y, 0);
		Button7.transform.position = new Vector3 (Button7.transform.position.x - Screen.width, Button7.transform.position.y, 0);
		Button8.transform.position = new Vector3 (Button8.transform.position.x - Screen.width, Button8.transform.position.y, 0);
		ButtonInfinity.transform.position = new Vector3 (ButtonInfinity.transform.position.x - Screen.width, ButtonInfinity.transform.position.y, 0);
		MoveChooseTimesOn = true;
		SetTimeText.SetActive (false);
		//VisualFrame.SetActive (true);
	}

	public void TimeChosen (int minutes) {
		RealTimer.GetComponent<TimerManager> ().Infinite = false;
		PlayerPrefs.SetInt ("DefaultTime", minutes);
		RealTimer.SendMessage ("TimerSet", minutes * 60);
		TotalTime.GetComponent<Text> ().text = minutes + " Minutes";
		MoveChooseTimesOn = false;
		TurnMoveChooseTimesOff = true;
		SetTimeText.SetActive (true);
		//VisualFrame.SetActive (false);
	}

	public void InfiniteTime () {
		PlayerPrefs.SetInt ("DefaultTime", 10); //10 Stands for Infinite Time
		RealTimer.GetComponent<TimerManager> ().InfiniteTime ();
		TotalTime.GetComponent<Text> ().text = "Infinite";
		MoveChooseTimesOn = false;
		TurnMoveChooseTimesOff = true;
		SetTimeText.SetActive (true);
		//VisualFrame.SetActive (false);
	}

	public void ClickedOff () {
		
		MoveChooseTimesOn = false;
		TurnMoveChooseTimesOff = true;
		SetTimeText.SetActive (true);
		//VisualFrame.SetActive (false);
	}



}
