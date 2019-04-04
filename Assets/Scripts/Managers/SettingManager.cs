using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;

public class SettingManager : MonoBehaviour
{

    private static SettingManager instance;
    public static SettingManager Instance { get { return instance; } }

    public GameObject soundButton;
    public GameObject googleServiceButton;


    public Sprite soundOffSprite;
    public Sprite soundOnSprite;

    public Image googlePlayIcon;


    void Awake()
    {
        instance = this;
    }


    // Use this for initialization
    void Start()
    {
        if (SaveManager.Instance.state.sound == true)
        {
            soundButton.transform.GetChild(0).GetComponent<Image>().sprite = soundOnSprite;
        }
        else
        {
            soundButton.transform.GetChild(0).GetComponent<Image>().sprite = soundOffSprite;
        }

        if (Social.localUser.authenticated == true)
        {
            googleServiceButton.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }
        else if (Social.localUser.authenticated == false)
        {
            googleServiceButton.transform.GetChild(0).GetComponent<Image>().color = Color.black;
        }

    }

    public void Sound()
    {

        SaveManager.Instance.state.sound = !SaveManager.Instance.state.sound;

        if (SaveManager.Instance.state.sound == true)
        {
            soundButton.transform.GetChild(0).GetComponent<Image>().sprite = soundOnSprite;
        }
        else
        {
            soundButton.transform.GetChild(0).GetComponent<Image>().sprite = soundOffSprite;
        }

        SaveManager.Instance.Save();

       
    }

    public void GoogleService()
    {
        if (Social.localUser.authenticated == false)
        {
            GooglePlayManager.Instance.SignIn();
        }
        else if (Social.localUser.authenticated == true)
        {
            GooglePlayManager.Instance.SignOut();
        }
    }




}
