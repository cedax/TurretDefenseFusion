using UnityEngine;
using TMPro;

public class ManagerVida : Singleton<ManagerVida>{
    [SerializeField] private TextMeshProUGUI vidaText;
    [SerializeField] private int vidaInicial;

    private int vida;
    public int Vida => vida;

    private void Start(){
        vida = vidaInicial;
        vidaText.text = vida.ToString();
    }

    public void RestarVida(int daño){
        vida -= daño;
        vidaText.text = vida.ToString();
    }

    public void SumarVida(int vida){
        this.vida += vida;
        vidaText.text = this.vida.ToString();
    }

    private void Update() {
        if(vida <= 0){
            GameManager.Instancia.PerderJuego();
        }
    }
}
