using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{

    private static AdManager instance;
    public static AdManager Instance { get { return instance; } }

    public int rewardAmount;
    public Text amountText;

    public Text starText;

    public bool isCollectingStars;
    public float previousStars;
    public float newAmount;

   

    void Start()
    {
        instance = this;
    }

    //UNITY ADS
    public void ShowAd(string _ad)
    {

        if (Advertisement.IsReady())
        {

            if (_ad == "rewardedVideo")
            {
                GameObject.Find("Canvas").GetComponent<GraphicRaycaster>().enabled = false;
                Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
            }
            else if (_ad == "video")
            {
                Advertisement.Show("video");
            }
        }
    }

    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                CollectReward();

                GameObject.Find("Canvas").GetComponent<GraphicRaycaster>().enabled = true;

                break;
            case ShowResult.Skipped:
                GameObject.Find("Canvas").GetComponent<GraphicRaycaster>().enabled = true;
                Debug.Log("Player did not fully watch the ad");
                break;
            case ShowResult.Failed:
                GameObject.Find("Canvas").GetComponent<GraphicRaycaster>().enabled = true;
                Debug.Log("Player has failed to launch the ad. Internet?");
                break;
        }
    }

    void Update()
    {
        if (isCollectingStars == true)
        {
            if (newAmount != previousStars + rewardAmount)
            {
                IncreaseCounter(1);
            }

            else if(isCollectingStars == true)
            {
                isCollectingStars = false;
                StartCoroutine(Wait());

            }

        }

    }

    public void CollectReward()
    {
        rewardAmount = 50;

        previousStars = SaveManager.Instance.state.star;
        newAmount = SaveManager.Instance.state.star;

        isCollectingStars = true;

        amountText.text = "+" + rewardAmount.ToString();

        SaveManager.Instance.state.star += rewardAmount;

        SaveManager.Instance.Save();

        ShopManagerNew.Instance.AddStar(0); //Updating Text

        Debug.Log("Player gain " + rewardAmount.ToString() + " amount of stars");

        amountText.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);


    }


    public void AddStar(int _amount)
    {
        SaveManager.Instance.state.star += _amount;
        starText.text = SaveManager.Instance.state.star.ToString();
    }

    public void IncreaseCounter(int _amount)
    {
        newAmount += _amount;
        starText.text = newAmount.ToString();
    }

    IEnumerator Wait()
    {

        yield return new WaitForSeconds(0.5f);

        amountText.gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);

        

    }


}
