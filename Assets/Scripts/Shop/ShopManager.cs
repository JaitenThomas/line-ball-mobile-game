using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ShopManager : MonoBehaviour {
	private static ShopManager instance;
	public static ShopManager Instance{get{return instance;}}


	public GameObject ShopButtonPrefab;

	public GameObject ballShopButtonContainer;
	public GameObject lineShopButtonContainer;

	public Text starText;

	public Sprite[] ballTextures;
	public Material ballMaterial;

	public Sprite[] lineTextures;
	public Material lineMaterial;

	public GameObject ball;
	public GameObject line;

	public GameObject ballShop;
	public GameObject lineShop;

	public int[] ballCosts;	
	public int[] lineCosts;

	public List<GameObject> ballButtons;
	public List<GameObject> lineButtons;

	public Image ballShopButtonSelectImage;
	public Image lineShopButtonSelectImage;

	public GameObject ShopPanel;
	public GameObject AdSelectionPanel;
	public Button starAddButton;
	public GameObject homeButton;
	public GameObject backToShopButton;




	void Awake(){
		instance = this;
	}

	// Use this for initialization
	public void Start () {

		ball = GameObject.Find ("Ball").gameObject;

		starText.text = SaveManager.Instance.state.star.ToString();

		ballShop.SetActive (true);
		lineShop.SetActive (false);



		int ballTextureIndex = 0;
		foreach (Sprite texture in ballTextures) {
			GameObject container = Instantiate (ShopButtonPrefab) as GameObject;
			container.transform.GetChild (0).GetComponent<Image> ().sprite = texture;
			container.transform.GetChild (0).GetComponent<RectTransform> ().sizeDelta = new Vector2 (texture.pixelsPerUnit, texture.pixelsPerUnit);

			if(container.transform.GetChild (0).GetComponent<RectTransform>().sizeDelta.x == 256){
				container.transform.GetChild (0).GetComponent<RectTransform> ().sizeDelta = new Vector2(50, 50);
			}

			if(container.transform.GetChild (0).GetComponent<RectTransform>().sizeDelta.x == 512){
				container.transform.GetChild (0).GetComponent<RectTransform> ().sizeDelta = new Vector2(25, 25);
			}

			container.transform.SetParent (ballShopButtonContainer.transform, false);

			int index = ballTextureIndex;
			container.transform.GetComponent<Button> ().onClick.AddListener (() => ChangeBallSkin (index));
			container.transform.GetChild (2).GetChild (0).GetComponent<Text> ().text = ballCosts [index].ToString ();
			if ((SaveManager.Instance.state.ballSkinAvailablility & 1 << index) == 1 << index) {
				container.transform.GetChild (2).gameObject.SetActive (false);
			} 
			ballTextureIndex++;
		}

		//Sprite[] textures = Resources.LoadAll<Sprite>("Player");
		int lineTextureIndex = 0;
	 	foreach(Sprite texture in lineTextures){
			GameObject container = Instantiate (ShopButtonPrefab) as GameObject;
			container.transform.GetChild(0).GetComponent<Image> ().sprite = texture;
			container.transform.SetParent (lineShopButtonContainer.transform, false);

			int index = lineTextureIndex;
			container.GetComponent<Button> ().onClick.AddListener(() => ChangeLineSkin(index));
			container.transform.GetChild (2).GetChild(0).GetComponent<Text>().text = lineCosts[index].ToString();
			if ((SaveManager.Instance.state.lineSkinAvailablility & 1 << index) == 1 << index) {
				container.transform.GetChild (2).gameObject.SetActive (false);
			} 
			lineTextureIndex++;
		}

		for (int i = 0; i < ballTextures.Length; i++) {
			ballButtons.Add(ballShopButtonContainer.transform.GetChild (i).gameObject);
		}

		for (int i = 0; i < lineTextures.Length; i++) {
			lineButtons.Add(lineShopButtonContainer.transform.GetChild (i).gameObject);
		}

		ChangeBallSkin (SaveManager.Instance.state.currentBallSkinIndex);
		ChangeLineSkin (SaveManager.Instance.state.currentLineSkinIndex);




	}

	private void ChangeBallSkin(int index)
	{
		if ((SaveManager.Instance.state.ballSkinAvailablility & 1 << index) == 1 << (32 - index))
		{
			

			ball.GetComponent<SpriteRenderer> ().sprite = ballTextures [index];


			for (int i = 0; i < ballButtons.Count; i++) {
				ballButtons [i].transform.GetChild (1).GetComponent<Image> ().color = Color.white;

				if (i == index) {
					ballButtons [index].transform.GetChild (1).GetComponent<Image> ().color = Color.green;
				} 
			}



			SaveManager.Instance.state.currentBallSkinIndex = index;
			SaveManager.Instance.Save ();
		}
		else 
		{
			//You do not have the skin, do you want to but it?
			int ballCost = ballCosts[index];

			if(SaveManager.Instance.state.star >= ballCost)
			{
				SaveManager.Instance.state.star -= ballCost;
				SaveManager.Instance.state.ballSkinAvailablility += 1 << index;
				starText.text = SaveManager.Instance.state.star.ToString();

				ballShopButtonContainer.transform.GetChild (index).GetChild(2).gameObject.SetActive(false);

				ChangeBallSkin (index);
				SaveManager.Instance.Save (); 	

			}
		}
	}

	private void ChangeLineSkin(int index)
	{
		if ((SaveManager.Instance.state.lineSkinAvailablility & 1 << index) == 1 << index)
		{
			lineMaterial.mainTexture = lineTextures [index].texture;



			for (int i = 0; i < lineButtons.Count; i++) {
				lineButtons [i].transform.GetChild (1).GetComponent<Image> ().color = Color.white;

				if (i == index) {
					lineButtons [index].transform.GetChild (1).GetComponent<Image> ().color = Color.green;
				} 
			}


			SaveManager.Instance.state.currentLineSkinIndex = index;
			SaveManager.Instance.Save ();
		}
		else 
		{
			//You do not have the skin, do you want to but it?
			int lineCost = lineCosts[index];

			if(SaveManager.Instance.state.star >= lineCost)
			{
				SaveManager.Instance.state.star -= lineCost;
				SaveManager.Instance.state.lineSkinAvailablility += 1 << index;
				starText.text = SaveManager.Instance.state.star.ToString();

				lineShopButtonContainer.transform.GetChild (index).GetChild(2).gameObject.SetActive(false);


				ChangeLineSkin (index);
				SaveManager.Instance.Save ();

			}
		}
	}

	public void Menu(){
		SaveManager.Instance.Save ();
		SceneManager.LoadScene ("Main Menu");
	}

	public void BallShop(){
		ballShop.SetActive (true);
		lineShop.SetActive (false);

		ballShopButtonSelectImage.color = Color.green;
		lineShopButtonSelectImage.color = Color.white;
	}

	public void LineShop(){
		ballShop.SetActive (false);
		lineShop.SetActive (true);

		ballShopButtonSelectImage.color = Color.white;
		lineShopButtonSelectImage.color = Color.green;
	}

	public void AddStar(int _amount){
		SaveManager.Instance.state.star += _amount;
		starText.text = SaveManager.Instance.state.star.ToString();
	}

	public void StarAdSelection(){
		AdSelectionPanel.gameObject.SetActive (true);
		ShopPanel.gameObject.SetActive (false);
		starAddButton.gameObject.SetActive (false);
		homeButton.SetActive (false);
		backToShopButton.SetActive (true);
	}

	public void Back(){
		SaveManager.Instance.Save ();
		AdSelectionPanel.gameObject.SetActive (false);
		ShopPanel.gameObject.SetActive (true);
		starAddButton.gameObject.SetActive (true);
		homeButton.SetActive (true);
		backToShopButton.SetActive (false);
		starText.text = SaveManager.Instance.state.star.ToString();

	}

}
