using UnityEngine;

public class GameOver : Singleton<GameOver>{
    [SerializeField] private GameObject canvasPrincipal;

    public void Perdiste(){
        canvasPrincipal.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void Start() {
        AnuncioBonificado.Instancia.OnUserEarnedRewardEvent.AddListener(IniciarJuego);
    }

    public void ReanudarJuego(){
        AnuncioBonificado.Instancia.anuncioParaReiniciarJuego = true;
        AnuncioBonificado.Instancia.anuncioParaRecibirVida = false;
        AnuncioBonificado.Instancia.anuncioParaRecibirVelocidad = false;
        AnuncioBonificado.Instancia.RequestAndLoadRewardedInterstitialAd();
        AnuncioBonificado.Instancia.ShowRewardedInterstitialAd();
    }

    public void IniciarJuego(){
        if(AnuncioBonificado.Instancia.anuncioParaReiniciarJuego == true){
            AnuncioBonificado.Instancia.anuncioParaReiniciarJuego = false;
            canvasPrincipal.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
            Time.timeScale = 1;
            ControlEnemigos.Instancia.enemigos.Clear();
            ManagerVida.Instancia.SumarVida(250);
            BalanceoJuego.Instancia.multiplicadorDeVelocidad = 0;
        }
    }
}