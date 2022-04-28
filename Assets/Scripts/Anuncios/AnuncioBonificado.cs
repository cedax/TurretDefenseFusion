using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class AnuncioBonificado : Singleton<AnuncioBonificado> {
    public bool anuncioParaReiniciarJuego = false;
    public bool anuncioParaRecibirVida = false;
    public bool anuncioParaRecibirVelocidad = false;
    private RewardedInterstitialAd rewardedInterstitialAd;
    public UnityEvent OnUserEarnedRewardEvent;

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
    public void RequestAndLoadRewardedInterstitialAd()
    {
        PrintStatus("Requesting Rewarded Interstitial ad.");

        string adUnitId = "ca-app-pub-7489440525451250/9692642676";

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