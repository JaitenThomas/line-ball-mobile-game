using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.Purchasing;

public class GooglePlayManager : MonoBehaviour
{
    private static GooglePlayManager instance;
    public static GooglePlayManager Instance { get { return instance; } }


   

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

    }
    void Start()
    {

        PlayGamesPlatform.Activate();
        //PlayGamesPlatform.DebugLogEnabled = true;

        if (Social.localUser.authenticated == false && SaveManager.Instance.state.firstLogin == false)
        {
            SaveManager.Instance.state.firstLogin = true;
            SignIn();
            SaveManager.Instance.Save();
        }
    }

    public void ShowAchievements()
    {
        if (Social.localUser.authenticated == true)
        {
            Social.Active.ShowAchievementsUI();
        }
        else if (Social.localUser.authenticated == false)
        {
            SignIn();
            Social.Active.ShowAchievementsUI();
        }

    }

    public void ShowLeaderboard()
    {

        if (Social.localUser.authenticated == true)
        {
            Social.Active.ShowLeaderboardUI();
        }
        else if (Social.localUser.authenticated == false)
        {
            SignIn();
            Social.Active.ShowLeaderboardUI();
        }
    }

    public void GetAchievement(string id)
    {
        Social.ReportProgress(id, 100, (bool success) => { });
    }

    public void SignIn()
    {

        if (Social.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success == true)
                {
                    SaveManager.Instance.state.googleService = true;
                    //IAPManager.Instance.RestorePurchases();

                    SettingManager.Instance.googlePlayIcon.color = Color.white;
                    SaveManager.Instance.Save();
                }

                else if (success == false)
                {
                    SettingManager.Instance.googlePlayIcon.color = Color.black;
                    SaveManager.Instance.state.googleService = false;
                    SaveManager.Instance.Save();
                }
            });
        }

      
    }

    public void SignOut()
    {
        if (Social.localUser.authenticated == true)
        {
            PlayGamesPlatform.Instance.SignOut();

            SaveManager.Instance.state.googleService = false;

            SettingManager.Instance.googlePlayIcon.color = Color.black;
            
            SaveManager.Instance.Save();
        }
    }
}
