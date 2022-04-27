using UnityEngine;
using TMPro;

public class AnunciosMonedas : MonoBehaviour
{

    private void Start() {
        AnuncioBonificado.Instancia.OnUserEarnedRewardEvent.AddListener(DarItem);
    }

    public void AnuncioVida(){
        AnuncioBonificado.Instancia.anuncioParaRecibirVida = true;
        AnuncioBonificado.Instancia.anuncioParaRecibirVelocidad = false;
        AnuncioBonificado.Instancia.anuncioParaReiniciarJuego = false;
        AnuncioBonificado.Instancia.RequestAndLoadRewardedInterstitialAd();
        AnuncioBonificado.Instancia.ShowRewardedInterstitialAd();
    }

    public void AnuncioVelocidad(){
        AnuncioBonificado.Instancia.anuncioParaRecibirVida = false;
        AnuncioBonificado.Instancia.anuncioParaRecibirVelocidad = true;
        AnuncioBonificado.Instancia.anuncioParaReiniciarJuego = false;
        AnuncioBonificado.Instancia.RequestAndLoadRewardedInterstitialAd();
        AnuncioBonificado.Instancia.ShowRewardedInterstitialAd();
    }

    public void DarItem(){
        if(AnuncioBonificado.Instancia.anuncioParaRecibirVida){
            AnuncioBonificado.Instancia.anuncioParaRecibirVida = false;
            ManagerVida.Instancia.SumarVida(250);
        }else if(AnuncioBonificado.Instancia.anuncioParaRecibirVelocidad){
            AnuncioBonificado.Instancia.anuncioParaRecibirVelocidad = false;
            BalanceoJuego.Instancia.multiplicadorVelocidadDisparo = 0.13f;
            Invoke("RestaurarVelocidad", 30f);
        }
    }

    private void RestaurarVelocidad(){
        BalanceoJuego.Instancia.multiplicadorVelocidadDisparo = 0f;
    }
}
