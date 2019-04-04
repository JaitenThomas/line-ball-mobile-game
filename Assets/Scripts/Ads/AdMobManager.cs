using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using System;

public class AdMobManager : MonoBehaviour {

	private static AdMobManager instance;
	public static AdMobManager Instance { get{return instance;}}

	public BannerView bannerView;

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);

		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	public void Start(){

		MobileAds.Initialize ("ca-app-pub-8755169433028902~6235081962"); // My App ID

       
            RequestBanner();
        
   

		//Debug.Log ("Create");
	}
	
	public void RequestBanner(){
		
		//string adUnitId = "ca-app-pub-3940256099942544/6300978111"; Test Banner
		string adUnitId = "ca-app-pub-8755169433028902/9083115893"; // Real Banner

		bannerView = new BannerView (adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        /*
		AdRequest request = new AdRequest.Builder ().AddTestDevice(AdRequest.TestDeviceSimulator)
			.AddTestDevice("5313081B348224A4CA2D3E5633D77693").Build ();
        */

        AdRequest request = new AdRequest.Builder().Build();

        if (SaveManager.Instance.state.broughtAds == false)
        {
            bannerView.LoadAd(request);
            bannerView.OnAdLoaded += HandleOnAdLoaded;
        }
	}

	void HandleOnAdLoaded(object a, EventArgs args)
	{
		print("loaded");
		ShowBanner ();
	} 

	public void ShowBanner()
	{
		bannerView.Show ();
	}

	public void HideBanner()
	{
		bannerView.Hide ();
	}
}
