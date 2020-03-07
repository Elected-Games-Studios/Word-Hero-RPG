using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    
    //instance it so you can always cll this script
    public static AdManager instance;
    //types of ads by name to reference
    public BannerView mainBanner;
    public InterstitialAd deathAd;
    //IDs by name
    private string testBannerId = "ca-app-pub-3940256099942544/6300978111";
    private string mainBannerId = "ca-app-pub-2837887988511984/6657302689";

    private string testDeathAdId = "ca-app-pub-3940256099942544/1033173712";
    private string deathAdId = "ca-app-pub-2837887988511984/9520227602";
    public void Indestructable()
    {
        string  appId = "ca-app-pub-2837887988511984~2635486563";
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        //dont want it to destroy itself when you change scenes
        DontDestroyOnLoad(gameObject);
        //Initialize the Google Mobile Ads SDK statuses are here also for you to check through
        Debug.Log("initializing");
        MobileAds.Initialize(appId);
        Debug.Log("mobileads on");
    }

public void RequestBanner()
    {
        //if there is a banner already(there shouldnt be but it could be there if an ad failed to load) destroy it.  otherwise it will fill RAM with its corpse
        if (this.mainBanner != null)
        {
            Debug.Log("destroying old banner");
            this.mainBanner.Destroy();
        }
        Debug.Log("config ad");
        //for specific banner sizes
        //AdSize adSize = new AdSize(200, 100); 
        mainBanner = new BannerView(mainBannerId, AdSize.Banner, AdPosition.Bottom);
        //creat an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        Debug.Log("ad req building");
        //Load the banner with that request, so put them together now.
        mainBanner.LoadAd(request);
        Debug.Log("Requesting Ad");
       

    }
public void RequestDeathAd()
    {
        if (this.deathAd != null)
        {
            Debug.Log("destroying old death ad");
            this.deathAd.Destroy();
        }
        //instantiate empty int ad
        deathAd = new InterstitialAd(deathAdId);
        Debug.Log("making new DeathAd");
        //creaty empty ad request
        AdRequest deathAdRequest = new AdRequest.Builder().Build();
        Debug.Log("requesting deathad");
        //Load the interstitial ad Request
        deathAd.LoadAd(deathAdRequest);
        Debug.Log("Fullfilling Deathad");
    }
}
