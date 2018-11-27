using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LocalNotification = UnityEngine.iOS.LocalNotification;
//using UnityEngine.iOS;

public class NotificationSystem : MonoBehaviour {

	//UnityEngine.iOS.LocalNotification notif;
	LocalNotification newNotif;

	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep; //Stop Dimming
	//	notif = new UnityEngine.iOS.LocalNotification();
		newNotif = new LocalNotification ();


		if (SceneManager.GetActiveScene().name == "Home") {
			

			if (PlayerPrefs.GetInt ("New") == 0) //0 = New
				RegisterForNotifications ();
			else
				PlayerPrefs.SetInt ("New", 1); //1 = Returning

			if (UnityEngine.iOS.NotificationServices.localNotificationCount > 0 || UnityEngine.iOS.NotificationServices.scheduledLocalNotifications.Length > 0) { //If open app & Notification Scheduled
				UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications(); //Clear it
			}

			//Instant Set Notif usually goes here!
		}

	}

	void RegisterForNotifications () {
		UnityEngine.iOS.NotificationServices.RegisterForNotifications (UnityEngine.iOS.NotificationType.Alert |
		UnityEngine.iOS.NotificationType.Badge |
		UnityEngine.iOS.NotificationType.Sound);
	}

	void SetShortNewNotif () { //1 Hour
		newNotif.fireDate = System.DateTime.Now.AddMinutes (1);
		newNotif.alertBody = "The clock's still ticking! Come back!";
		UnityEngine.iOS.NotificationServices.ScheduleLocalNotification (newNotif);
	}
	void SetMidNewNotif () { //2 Hour
		newNotif.fireDate = System.DateTime.Now.AddHours (2);
		newNotif.alertBody = "Come back! Any time is the right time to practice your speaking skills!";
		UnityEngine.iOS.NotificationServices.ScheduleLocalNotification (newNotif);
	}
	void SetLongNewNotif () { //2 Days
		newNotif.fireDate = System.DateTime.Now.AddDays (2);
		newNotif.alertBody = "Come back! Any time is the right time to practice your speaking skills!";
		UnityEngine.iOS.NotificationServices.ScheduleLocalNotification (newNotif);
	}
		
	void OnApplicationQuit () {
		UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications(); //Clear it
		SetMidNewNotif (); 
		SetLongNewNotif ();
	} 

	void OnApplicationFocus (bool focus) {
		if (!focus) {
			UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications (); //Clear it
			SetShortNewNotif ();
			SetMidNewNotif ();
			SetLongNewNotif ();
		}
	}
	

}
