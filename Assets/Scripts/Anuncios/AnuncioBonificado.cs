using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class AnuncioBonificado : Singleton<AnuncioBonificado> {
    public bool anuncioParaReiniciarJuego = false;
    public bool anuncioParaRecibirVida = false;
    public bool anuncioParaRecibirVelocidad = false;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    private RewardedInterstitialAd rewardedInterstitialAd;
    public UnityEvent OnAdLoadedEvent;
    public UnityEvent OnAdFailedToLoadEvent;
    public UnityEvent OnAdOpeningEvent;
    public UnityEvent OnAdFailedToShowEvent;
    public UnityEvent OnUserEarnedRewardEvent;
    public UnityEvent OnAdClosedEvent;


    #region UNITY MONOBEHAVIOR METHODS

    public void Start()
    {
        MobileAds.SetiOSAppPauseOnBackground(true);
        MobileAds.Initialize(HandleInitCompleteAction);
    }

    private void HandleInitCompleteAction(InitializationStatus initstatus)
    {
        Debug.Log("Initialization complete.");

        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            Debug.Log("Initialization complete.");
            //RequestBannerAd();
        });
    }

    #endregion

    #region HELPER METHODS

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    #endregion

    #region REWARDED ADS

    public void RequestAndLoadRewardedAd()
    {
        PrintStatus("Requesting Rewarded ad.");
        #if UNITY_EDITOR
                string adUnitId = "unused";
        #elif UNITY_ANDROID
                string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        #elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-3940256099942544/1712485313";
        #else
                string adUnitId = "unexpected_platform";
        #endif

        // create new rewarded ad instance
        rewardedAd = new RewardedAd(adUnitId);

        // Add Event Handlers
        rewardedAd.OnAdLoaded += (sender, args) =>
        {
            PrintStatus("Reward ad loaded.");
            OnAdLoadedEvent.Invoke();
        };
        rewardedAd.OnAdFailedToLoad += (sender, args) =>
        {
            PrintStatus("Reward ad failed to load.");
            OnAdFailedToLoadEvent.Invoke();
        };
        rewardedAd.OnAdOpening += (sender, args) =>
        {
            PrintStatus("Reward ad opening.");
            OnAdOpeningEvent.Invoke();
        };
        rewardedAd.OnAdFailedToShow += (sender, args) =>
        {
            PrintStatus("Reward ad failed to show with error: "+args.AdError.GetMessage());
            OnAdFailedToShowEvent.Invoke();
        };
        rewardedAd.OnAdClosed += (sender, args) =>
        {
            PrintStatus("Reward ad closed.");
            OnAdClosedEvent.Invoke();
        };
        rewardedAd.OnUserEarnedReward += (sender, args) =>
        {
            PrintStatus("User earned Reward ad reward: "+args.Amount);
            //OnUserEarnedRewardEvent.Invoke();
        };
        rewardedAd.OnAdDidRecordImpression += (sender, args) =>
        {
            PrintStatus("Reward ad recorded an impression.");
        };
        rewardedAd.OnPaidEvent += (sender, args) =>
        {
            string msg = string.Format("{0} (currency: {1}, value: {2}",
                                        "Rewarded ad received a paid event.",
                                        args.AdValue.CurrencyCode,
                                        args.AdValue.Value);
            PrintStatus(msg);
        };

        // Create empty ad request
        rewardedAd.LoadAd(CreateAdRequest());
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Show();
        }
        else
        {
            PrintStatus("Rewarded ad is not ready yet.");
        }
    }

    public void RequestAndLoadRewardedInterstitialAd()
    {
        PrintStatus("Requesting Rewarded Interstitial ad.");

        // These ad units are configured to always serve test ads.
        #if UNITY_EDITOR
                string adUnitId = "unused";
        #elif UNITY_ANDROID
                    string adUnitId = "ca-app-pub-3940256099942544/5354046379";
        #elif UNITY_IPHONE
                    string adUnitId = "ca-app-pub-3940256099942544/6978759866";
        #else
                    string adUnitId = "unexpected_platform";
        #endif

        // Create an interstitial.
        RewardedInterstitialAd.LoadAd(adUnitId, CreateAdRequest(), (rewardedInterstitialAd, error) =>
        {
            if (error != null)
            {
                PrintStatus("Rewarded Interstitial ad load failed with error: " + error);
                NotificacionAnuncios.Instancia.MostrarNotificacion("No hay anuncios disponibles");
                return;
            }

            this.rewardedInterstitialAd = rewardedInterstitialAd;
            PrintStatus("Rewarded Interstitial ad loaded.");

            // Register for ad events.
            this.rewardedInterstitialAd.OnAdDidPresentFullScreenContent += (sender, args) =>
            {
                PrintStatus("Rewarded Interstitial ad presented.");
            };
            this.rewardedInterstitialAd.OnAdDidDismissFullScreenContent += (sender, args) =>
            {
                this.rewardedInterstitialAd = null;
            };
            this.rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += (sender, args) =>
            {
                PrintStatus("Rewarded Interstitial ad failed to present with error: "+
                                                                        args.AdError.GetMessage());
                this.rewardedInterstitialAd = null;
                NotificacionAnuncios.Instancia.MostrarNotificacion("No se pudo cargar el anuncio");
            };
            this.rewardedInterstitialAd.OnPaidEvent += (sender, args) =>
            {
                string msg = string.Format("{0} (currency: {1}, value: {2}",
                                            "Rewarded Interstitial ad received a paid event.",
                                            args.AdValue.CurrencyCode,
                                            args.AdValue.Value);
                PrintStatus(msg);
            };
            this.rewardedInterstitialAd.OnAdDidRecordImpression += (sender, args) =>
            {
                PrintStatus("Rewarded Interstitial ad recorded an impression.");
            };
        });
    }

    public void ShowRewardedInterstitialAd()
    {
        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Show((reward) =>
            {
                PrintStatus("Rewarded Interstitial ad Rewarded : " + reward.Amount);
                OnUserEarnedRewardEvent.Invoke();
            });
        }
        else
        {
            PrintStatus("Rewarded Interstitial ad is not ready yet.");
        }
    }

    #endregion

    #region Utility

    private void PrintStatus(string message)
    {
        Debug.Log(message);
        MobileAdsEventExecutor.ExecuteInUpdate(() => {
            Debug.Log(message);
        });
    }

    #endregion
}