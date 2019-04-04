using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LineShopButton : MonoBehaviour {

	public GameObject overlay;

	public Sprite colorSprite;

	public int index;

	public bool isDefault;

	public int cost;

	public Image boarder;


	public ShopManagerNew sm;


	// Use this for initialization
	void Start () {
		if (ShopManagerNew.Instance.lineBrought [index] == true || isDefault == true) {
			overlay.gameObject.SetActive (false);
		} else {
			overlay.gameObject.SetActive (true);
		}
	
		//Changes the overlay text to the cost count
		gameObject.transform.GetChild (2).GetChild(0).GetComponent<Text>().text = cost.ToString();

		if(isDefault == true){
			ShopManagerNew.Instance.lineBrought[index] = true;
		}

		
		if (index == SaveManager.Instance.state.lastLineIndex) {
			gameObject.transform.GetChild (1).GetComponent<Image> ().color = Color.green;
		} else {
			gameObject.transform.GetChild (1).GetComponent<Image>().color = Color.white;
		}



	}

	public void Buy(){
		if (SaveManager.Instance.state.star >= cost && ShopManagerNew.Instance.lineBrought[index] == false)
		{
			SaveManager.Instance.state.star -= cost;

			GameObject.FindObjectOfType<ShopManagerNew> ().starText.text = SaveManager.Instance.state.star.ToString();

			ShopManagerNew.Instance.lineBrought[index] = true;

			overlay.gameObject.SetActive (false);

			SaveManager.Instance.state.linesBrought += 1;

			if(SaveManager.Instance.state.linesBrought >= 1){
				GooglePlayManager.Instance.GetAchievement ("CgkItpiq1OgVEAIQDQ");
			}
			Equip ();
		}
		else 
		{
			if (ShopManagerNew.Instance.lineBrought[index] == true) 
			{
				ShopManagerNew.Instance.lineBrought[index] = true;
				overlay.gameObject.SetActive (false);
				Equip ();
			}
		}



	}

	void Equip(){

		sm.lineButtons [index].gameObject.transform.GetChild (1).GetComponent<Image>().color = Color.green;

		for (int i = 0; i < sm.lineButtons.Count; i++) {
			if(i != index){
				sm.lineButtons [i].gameObject.transform.GetChild (1).GetComponent<Image>().color = Color.white;
			}
		}

		SaveManager.Instance.state.lastLineIndex = index;

		SaveManager.Instance.state.lineIndex = index;

		sm.lineMaterial.mainTexture = transform.GetChild (0).GetComponent<Image>().sprite.texture; 

		SaveManager.Instance.Save ();

	}


}
