using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AnuncioBonificado : Singleton<AnuncioBonificado>{
    private RewardedAd rewardedAd;
    //private string RewardedAdID = "ca-app-pub-3940256099942544/5354046379";
    private string RewardedAdID = "ca-app-pub-3940256099942544/8691691433";
    public InterstitialAd interstitial;

    private void Start() {
        MobileAds.Initialize(initStatus => { });
        this.interstitial = new InterstitialAd(RewardedAdID);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }
}
