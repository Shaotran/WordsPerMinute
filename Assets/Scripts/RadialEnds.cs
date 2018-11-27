using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Radians To Degrees:
//radians*pi/180

public class RadialEnds : MonoBehaviour {

	public GameObject RadialBar;
	float angle;
	float radius;
	float x;
	float y;

	void Start () {
		radius = gameObject.GetComponent<RectTransform>().position.y - RadialBar.GetComponent<RectTransform>().position.y;
	}
	
	void Update () {
		if (gameObject.name == "RadialEnd2") {
			angle = RadialBar.GetComponent<Image> ().fillAmount * 360;
			float x = radius * Mathf.Cos ((90 - angle) * Mathf.PI / 180);
			float y = radius * Mathf.Sin ((90 - angle) * Mathf.PI / 180);

			if (RadialBar.GetComponent<Image> ().fillClockwise == false)
				x = x * -1; //Negates x position if CounterClockwise

			gameObject.transform.position = new Vector3 (x, y, 0) + RadialBar.transform.position;
		}

		gameObject.GetComponent<RawImage> ().color = RadialBar.GetComponent<Image> ().color;
	}
}
