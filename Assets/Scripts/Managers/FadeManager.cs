using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour {

	public static FadeManager Instance{ set; get;}

	public Image fadeImage;
	private bool isInTransition;
	private float transition;
	private bool isShowing;
	private float duration;

	private void Awake(){
		DontDestroyOnLoad (this.gameObject);

		if(Instance == null){
			Instance = this;
		} else if(Instance != null){
			Destroy (this.gameObject);
		}

	}

	public void Fade(bool showing, float duration){
		isShowing = showing;
		isInTransition = true;
		this.duration = duration;
		transition = (isShowing) ? 0 : 1;
	}

	private void Update(){

		if(!isInTransition)
			return;
		if(isInTransition){
			transition += (isShowing) ? Time.deltaTime * (1/duration) : -Time.deltaTime * (1/duration);
			fadeImage.color = Color.Lerp (new Color(0, 0, 0, 0), Color.black, transition);
			fadeImage.raycastTarget = true;
		}

		if (transition > 1 || transition < 0) {
			isInTransition = false;
			fadeImage.raycastTarget = false;
		}
		
	}
}
