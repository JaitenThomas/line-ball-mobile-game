using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour {

	public GameObject overlay;
	public Sprite ballSprite;
	public int index;
	public bool isDefault;
	public int cost;
	public Image boarder;

	public ShopManagerNew sm;

	// Use this for initialization
	void Start () {
		
		if (ShopManagerNew.Instance.brought [index] == true || isDefault == true) {
			overlay.gameObject.SetActive (false);
		} else {
			overlay.gameObject.SetActive (true);
		}



		//Changes the overlay text to the cost;
		gameObject.transform.GetChild (2).GetChild(0).GetComponent<Text>().text = cost.ToString();



		if (isDefault == true) {
			ShopManagerNew.Instance.brought [index] = true;
			overlay.gameObject.SetActive (false);
		}
			
		if (index == SaveManager.Instance.state.lastIndex) {
			gameObject.transform.GetChild (1).GetComponent<Image> ().color = Color.green;
		} else {
			gameObject.transform.GetChild (1).GetComponent<Image>().color = Color.white;
		}

	}

	public void Buy(){
		if (SaveManager.Instance.state.star >= cost && ShopManagerNew.Instance.brought[index] == false) 
		{
			SaveManager.Instance.state.star -= cost;

			GameObject.FindObjectOfType<ShopManagerNew> ().starText.text = SaveManager.Instance.state.star.ToString ();

			ShopManagerNew.Instance.brought[index] = true;

			SaveManager.Instance.state.ballsBrought += 1;

			if(SaveManager.Instance.state.ballsBrought >= 1){
				GooglePlayManager.Instance.GetAchievement ("CgkItpiq1OgVEAIQDA");
			}

			overlay.gameObject.SetActive (false);
			Debug.Log ("Buy");
			Equip ();
		}
		else 
		{
			if (ShopManagerNew.Instance.brought [index] == true) {

				ShopManagerNew.Instance.brought [index] = true;
				overlay.gameObject.SetActive (false);
				Equip ();
			} else {
				Debug.Log ("Dont have enough gold");
			}
				
		}


	}

	void Equip(){

		sm.buttons [index].gameObject.transform.GetChild (1).GetComponent<Image>().color = Color.green;

		for (int i = 0; i < sm.buttons.Count; i++) {
			if(i != index){
				sm.buttons [i].gameObject.transform.GetChild (1).GetComponent<Image>().color = Color.white;
			}
		}

		SaveManager.Instance.state.lastIndex = index;
		SaveManager.Instance.state.ballIndex = index;

		GameObject.Find ("Ball").GetComponent<SpriteRenderer>().sprite = GameObject.Find ("Ball").GetComponent<PlayerManager>().playerSprites[SaveManager.Instance.state.ballIndex];
	
		SaveManager.Instance.Save ();
	}


}
