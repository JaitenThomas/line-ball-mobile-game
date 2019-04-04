using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWidthCamera : MonoBehaviour {

	public int targetWidth = 640;
	public float pixelsToUnit = 100;

   
    

	// Update is called once per frame
	void Awake () {
		int height = Mathf.RoundToInt (targetWidth / (float)Screen.width * Screen.height);

		Camera.main.orthographicSize = height / pixelsToUnit / 2;
	}
}
	