using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;


public class ShopManagerNew : MonoBehaviour {

	private static ShopManagerNew instance;
	public static ShopManagerNew Instance{get{return instance;}}

	public GameObject ballShopItemContainer;
	public GameObject lineShopItemContainer;

	public List<GameObject> buttons;
	public List<GameObject> lineButtons;

	public GameObject ballShop;
	public GameObject lineShop;

	public Text starText;

	public int buttonsCount;
	public Material lineMaterial;


	public Image ballShopButtonSelectImage;
	public Image lineShopButtonSelectImage;

	public GameObject ShopPanel;
	public GameObject AdSelectionPanel;
	public Button starAddButton;
	public GameObject homeButton;
	public GameObject backToShopButton;

	public List<bool> brought;
	public List<bool> lineBrought;

	public GameObject ball;

	public GameObject noAdButton;
	public GameObject doubleStarButton;

	public Text star1000PriceText;
	public Text star2000PriceText;
	public Text star3000PriceText;




	void Awake(){
		instance = this;
	}


	// Use this for initialization
	void Start () {
		ball = GameObject.Find ("Ball");

		ball.GetComponent<PlayerManager> ().ballAnimator.SetBool ("IsDead", false);
		ball.SetActive (true);



		#region
		brought.Add (SaveManager.Instance.state.item1);
		brought.Add (SaveManager.Instance.state.item2);
		brought.Add (SaveManager.Instance.state.item3);
		brought.Add (SaveManager.Instance.state.item4);
		brought.Add (SaveManager.Instance.state.item5);
		brought.Add (SaveManager.Instance.state.item6);
		brought.Add (SaveManager.Instance.state.item7);
		brought.Add (SaveManager.Instance.state.item8);
		brought.Add (SaveManager.Instance.state.item9);
		brought.Add (SaveManager.Instance.state.item10);
		brought.Add (SaveManager.Instance.state.item11);
		brought.Add (SaveManager.Instance.state.item12);
		brought.Add (SaveManager.Instance.state.item13);
		brought.Add (SaveManager.Instance.state.item14);
		brought.Add (SaveManager.Instance.state.item15);
		brought.Add (SaveManager.Instance.state.item16);
		brought.Add (SaveManager.Instance.state.item17);
		brought.Add (SaveManager.Instance.state.item18);
		brought.Add (SaveManager.Instance.state.item19);
		brought.Add (SaveManager.Instance.state.item20);
		brought.Add (SaveManager.Instance.state.item21);
		brought.Add (SaveManager.Instance.state.item22);
		brought.Add (SaveManager.Instance.state.item23);
		brought.Add (SaveManager.Instance.state.item24);
		brought.Add (SaveManager.Instance.state.item25);
		#endregion

		#region
		//single
		lineBrought.Add (SaveManager.Instance.state.lineItem1);
		lineBrought.Add (SaveManager.Instance.state.lineItem2);
		lineBrought.Add (SaveManager.Instance.state.lineItem3);
		lineBrought.Add (SaveManager.Instance.state.lineItem4);
		lineBrought.Add (SaveManager.Instance.state.lineItem5);
		lineBrought.Add (SaveManager.Instance.state.lineItem6);
		lineBrought.Add (SaveManager.Instance.state.lineItem7);
		lineBrought.Add (SaveManager.Instance.state.lineItem8);
		lineBrought.Add (SaveManager.Instance.state.lineItem9);
		lineBrought.Add (SaveManager.Instance.state.lineItem10);
		lineBrought.Add (SaveManager.Instance.state.lineItem11);
		lineBrought.Add (SaveManager.Instance.state.lineItem12);
		lineBrought.Add (SaveManager.Instance.state.lineItem13);
		lineBrought.Add (SaveManager.Instance.state.lineItem14);
		lineBrought.Add (SaveManager.Instance.state.lineItem15);
		lineBrought.Add (SaveManager.Instance.state.lineItem16);
		lineBrought.Add (SaveManager.Instance.state.lineItem17);
		lineBrought.Add (SaveManager.Instance.state.lineItem18);
		lineBrought.Add (SaveManager.Instance.state.lineItem19);
		lineBrought.Add (SaveManager.Instance.state.lineItem20);
		lineBrought.Add (SaveManager.Instance.state.lineItem21);
		lineBrought.Add (SaveManager.Instance.state.lineItem22);

		//double
		lineBrought.Add(SaveManager.Instance.state.lineItem23);
		lineBrought.Add(SaveManager.Instance.state.lineItem24); 
		lineBrought.Add(SaveManager.Instance.state.lineItem25); 
		lineBrought.Add(SaveManager.Instance.state.lineItem26); 
		lineBrought.Add(SaveManager.Instance.state.lineItem27); 
		lineBrought.Add(SaveManager.Instance.state.lineItem28); 
		lineBrought.Add(SaveManager.Instance.state.lineItem29); 
		lineBrought.Add(SaveManager.Instance.state.lineItem30); 
		lineBrought.Add(SaveManager.Instance.state.lineItem31); 
		lineBrought.Add(SaveManager.Instance.state.lineItem32); 
		lineBrought.Add(SaveManager.Instance.state.lineItem33); 
		lineBrought.Add(SaveManager.Instance.state.lineItem34); 
		lineBrought.Add(SaveManager.Instance.state.lineItem35); 
		lineBrought.Add(SaveManager.Instance.state.lineItem36); 
		lineBrought.Add(SaveManager.Instance.state.lineItem37); 
		lineBrought.Add(SaveManager.Instance.state.lineItem38); 
		lineBrought.Add(SaveManager.Instance.state.lineItem39); 
		lineBrought.Add(SaveManager.Instance.state.lineItem40); 
		lineBrought.Add(SaveManager.Instance.state.lineItem41); 
		lineBrought.Add(SaveManager.Instance.state.lineItem42); 
		lineBrought.Add(SaveManager.Instance.state.lineItem43); 

		#endregion


		starText.text = SaveManager.Instance.state.star.ToString();

		ballShop.SetActive (true);
		lineShop.SetActive (false);

		//SaveManager.Instance.Save ();

		for (int i = 0; i < buttons.Count; i++) {
            Debug.Log(i);
			buttons [i].GetComponent<ShopButton> ().index = i;
			buttons [i].GetComponent<ShopButton> ().ballSprite = ball.GetComponent<PlayerManager>().playerSprites[i];
			buttons [i].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = ball.GetComponent<PlayerManager>().playerSprites[i];
		}

        for (int i = 0; i < lineButtons.Count; i++)
        {
            lineButtons[i].GetComponent<LineShopButton>().index = i;
            lineButtons[i].GetComponent<LineShopButton>().colorSprite = ball.GetComponent<PlayerManager>().lineTextures[i];
            lineButtons[i].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = ball.GetComponent<PlayerManager>().lineTextures[i];
        }



        if (SaveManager.Instance.state.broughtAds == true && Social.localUser.authenticated == true) {
			noAdButton.transform.GetChild(0).GetComponent<Text> ().color = Color.white;
		} else if (SaveManager.Instance.state.broughtDoubledStars == false){
			noAdButton.transform.GetChild(0).GetComponent<Text> ().color = Color.black;
		}

		if (SaveManager.Instance.state.broughtDoubledStars == true && Social.localUser.authenticated == true) {
			doubleStarButton.transform.GetChild(0).GetComponent<Text> ().color = Color.white;
		} else if (SaveManager.Instance.state.broughtDoubledStars == false){
			doubleStarButton.transform.GetChild(0).GetComponent<Text> ().color = Color.black;
		}

	}


	public void Menu(){


		StartCoroutine (ToMenu());
	
	}

	IEnumerator ToMenu(){
		

		#region
		SaveManager.Instance.state.item1 = brought[0];
		SaveManager.Instance.state.item2 = brought[1];
		SaveManager.Instance.state.item3 = brought[2];
		SaveManager.Instance.state.item4 = brought[3];
		SaveManager.Instance.state.item5 = brought[4];
		SaveManager.Instance.state.item6 = brought[5];
		SaveManager.Instance.state.item7 = brought[6];
		SaveManager.Instance.state.item8 = brought[7];
		SaveManager.Instance.state.item9 = brought[8];
		SaveManager.Instance.state.item10 = brought[9];
		SaveManager.Instance.state.item11 = brought[10];
		SaveManager.Instance.state.item12 = brought[11];
		SaveManager.Instance.state.item13 = brought[12];
		SaveManager.Instance.state.item14 = brought[13];
		SaveManager.Instance.state.item15 = brought[14];
		SaveManager.Instance.state.item16 = brought[15];
		SaveManager.Instance.state.item17 = brought[16];
		SaveManager.Instance.state.item18 = brought[17];
		SaveManager.Instance.state.item19 = brought[18];
		SaveManager.Instance.state.item20 = brought[19];
		SaveManager.Instance.state.item21 = brought[20];
		SaveManager.Instance.state.item22 = brought[21];
		SaveManager.Instance.state.item23 = brought[22];
		SaveManager.Instance.state.item24 = brought[23];
		SaveManager.Instance.state.item25 = brought[24];
		#endregion

		#region
		//single
		SaveManager.Instance.state.lineItem1 = lineBrought[0];
		SaveManager.Instance.state.lineItem2 = lineBrought[1];
		SaveManager.Instance.state.lineItem3 = lineBrought[2];
		SaveManager.Instance.state.lineItem4 = lineBrought[3];
		SaveManager.Instance.state.lineItem5 = lineBrought[4];
		SaveManager.Instance.state.lineItem6 = lineBrought[5];
		SaveManager.Instance.state.lineItem7 = lineBrought[6];
		SaveManager.Instance.state.lineItem8 = lineBrought[7];
		SaveManager.Instance.state.lineItem9 = lineBrought[8];
		SaveManager.Instance.state.lineItem10 = lineBrought[9];
		SaveManager.Instance.state.lineItem11 = lineBrought[10];
		SaveManager.Instance.state.lineItem12 = lineBrought[11];
		SaveManager.Instance.state.lineItem13 = lineBrought[12];
		SaveManager.Instance.state.lineItem14 = lineBrought[13];
		SaveManager.Instance.state.lineItem15 = lineBrought[14];
		SaveManager.Instance.state.lineItem16 = lineBrought[15];
		SaveManager.Instance.state.lineItem17 = lineBrought[16];
		SaveManager.Instance.state.lineItem18 = lineBrought[17];
		SaveManager.Instance.state.lineItem19 = lineBrought[18];
		SaveManager.Instance.state.lineItem20 = lineBrought[19];
		SaveManager.Instance.state.lineItem21 = lineBrought[20];
		SaveManager.Instance.state.lineItem22 = lineBrought[21];

		//double
		SaveManager.Instance.state.lineItem23 = lineBrought[22];
		SaveManager.Instance.state.lineItem24 = lineBrought[23];
		SaveManager.Instance.state.lineItem25 = lineBrought[24];
		SaveManager.Instance.state.lineItem26 = lineBrought[25];
		SaveManager.Instance.state.lineItem27 = lineBrought[26];
		SaveManager.Instance.state.lineItem28 = lineBrought[27];
		SaveManager.Instance.state.lineItem29 = lineBrought[28];
		SaveManager.Instance.state.lineItem30 = lineBrought[29];
		SaveManager.Instance.state.lineItem31 = lineBrought[30];
		SaveManager.Instance.state.lineItem32 = lineBrought[31];
		SaveManager.Instance.state.lineItem33 = lineBrought[32];
		SaveManager.Instance.state.lineItem34 = lineBrought[33];
		SaveManager.Instance.state.lineItem35 = lineBrought[34];
		SaveManager.Instance.state.lineItem36 = lineBrought[35];
		SaveManager.Instance.state.lineItem37 = lineBrought[36];
		SaveManager.Instance.state.lineItem38 = lineBrought[37];
		SaveManager.Instance.state.lineItem39 = lineBrought[38];
		SaveManager.Instance.state.lineItem40 = lineBrought[39];
		SaveManager.Instance.state.lineItem41 = lineBrought[40];
		SaveManager.Instance.state.lineItem42 = lineBrought[41];
		SaveManager.Instance.state.lineItem43 = lineBrought[42];
		#endregion


		ball.transform.position = ball.gameObject.GetComponent<PlayerManager>().startPosition; // Move the player back to start position
		ball.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None; // Unfreeze all player constriants
		ball.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation; // Freezet the player rotation
		ball.transform.eulerAngles = new Vector3 (0,0,0); // Set the player rotation back to default
		SaveManager.Instance.Save ();

		FadeManager.Instance.Fade (true, 0.5f);
		yield return new WaitForSeconds (0.5f); // Wait .25 seconds half the animation
		FadeManager.Instance.Fade(false, 0.5f);

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