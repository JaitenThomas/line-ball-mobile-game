using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour {

    public static MainMenuManager instance;

	public GameObject mainMenuPanel;
	public GameObject shopPanel;
	public GameObject gameModePanel;
	public GameObject settingPanel;

	public Image ballShopButtonSelectImage;
	public Image lineShopButtonSelectImage;

	public Text classicHighScoreText;
	public Text gapHighScoreText;
	public Text hoopHighScoreText;
	public Text protectHighScoreText;
    public Text boostHighScoreText;

    public Color[] colorArray;


	public string sceneName;

    //Setting Controls
    public bool setting;
    public GameObject settingButton;
    public Button[] settingButtons;
    

    private void Awake()
    {
        instance = this;
    }

    void Start(){
		classicHighScoreText.text = SaveManager.Instance.state.classicHighScore.ToString ();
		gapHighScoreText.text = SaveManager.Instance.state.gapHighScore.ToString ();
		hoopHighScoreText.text = SaveManager.Instance.state.hoopHighScore.ToString ();
		protectHighScoreText.text = SaveManager.Instance.state.protectHighScore.ToString ();
        boostHighScoreText.text = SaveManager.Instance.state.boostHighScore.ToString();

        /*
		if (IAPManager.m_StoreController.products.WithID ("noads").hasReceipt == true) {
			SaveManager.Instance.state.broughtAds = true;
			AdMobManager.Instance.HideBanner ();
			SaveManager.Instance.Save ();

		} else if (IAPManager.m_StoreController.products.WithID ("noads").hasReceipt == false) {
			SaveManager.Instance.state.broughtAds = false;
			AdMobManager.Instance.ShowBanner ();
			SaveManager.Instance.Save ();

		}
		if (IAPManager.m_StoreController.products.WithID ("doubledstars").hasReceipt == true) {
			SaveManager.Instance.state.broughtDoubledStars = true;
			SaveManager.Instance.Save ();
		} else if (IAPManager.m_StoreController.products.WithID ("doubledstars").hasReceipt == false) {
			SaveManager.Instance.state.broughtDoubledStars = false;

			SaveManager.Instance.Save ();
		}
		*/


    }

    public void ToStorePage()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.J10.lineball");
    }

    public void ToggleSetting()
    {
        setting = !setting;

        if (setting == false)
        {
            foreach (Button item in settingButtons)
            {
                item.gameObject.SetActive(false);
                settingButton.GetComponent<Animator>().SetTrigger("Toggle");
            }
        }

        else if (setting == true)
        {
            foreach (Button item in settingButtons)
            {
                item.gameObject.SetActive(true);
                settingButton.GetComponent<Animator>().SetTrigger("Toggle");
            }
        }
    }

    public void Gamemodes(){
		StartCoroutine (WaitForGamemode());
	}

	IEnumerator WaitForGamemode(){
		FadeManager.Instance.Fade (true, 0.5f);
		yield return new WaitForSeconds (0.5f); // Wait .25 seconds half the animation
		FadeManager.Instance.Fade(false, 0.5f);
		gameModePanel.SetActive (true); // Show to gamemode panel
		mainMenuPanel.SetActive (false); // Unshow the main menu panel

	}

	public void Menu(){
		StartCoroutine (WaitForMenu());
	}

	IEnumerator WaitForMenu(){
		FadeManager.Instance.Fade (true, 0.5f);
		yield return new WaitForSeconds (0.5f); // Wait .25 seconds half the animation
		FadeManager.Instance.Fade(false, 0.5f);
		gameModePanel.SetActive (false); // Unshow to gamemode panel
		mainMenuPanel.SetActive (true); // Show the main menu panel
	}


    public void ChangeScene(string _name) {
        sceneName = _name;

        if (SaveManager.Instance.state.firstPlay == true && sceneName == "Classic")
        {
            sceneName = "TutorialPlay";


            SaveManager.Instance.state.firstPlay = false;
            SaveManager.Instance.Save();
        }

        else if (SaveManager.Instance.state.broughtAds == false)
        {
            if (_name != "Setting" && _name != "Shop" && _name != "Main Menu" && _name != "Tutorial")
            {
                AdMobManager.Instance.HideBanner();
            }
            else
            {
                AdMobManager.Instance.ShowBanner();
            }
        }

        StartCoroutine(WaitToChangeScene());

    }



    IEnumerator WaitToChangeScene(){
		FadeManager.Instance.Fade (true, 0.5f);
		yield return new WaitForSeconds (0.5f); // Wait .25 seconds half the animation
		FadeManager.Instance.Fade(false, 0.5f);
		SceneManager.LoadScene (sceneName);
	}
}
