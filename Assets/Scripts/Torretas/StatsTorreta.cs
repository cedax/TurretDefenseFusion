using UnityEngine;

public class StatsTorreta : MonoBehaviour {
    [SerializeField] private float vidaInicial;
    [SerializeField] private int nivel;
    public int indexTOrreta { get; private set; }
    public float vidaActual { get; set; }
    public float costoSiguienteNivel { get; set; }
    public float daño { get; private set; }
    public int Nivel => nivel;

    private void Awake(){
        vidaActual = vidaInicial;
        daño = BalanceoJuego.Instancia.dañoBaseDeTorretas * nivel;
        BalanceoJuego.Instancia.potenciaDeFuego += daño;
    }

    private void Update() {
        float dañoDeBalas = BalanceoJuego.Instancia.dañoBaseDeTorretas * nivel;
        if (daño != dañoDeBalas) {
            BalanceoJuego.Instancia.potenciaDeFuego -= daño;
            daño = dañoDeBalas;
            BalanceoJuego.Instancia.potenciaDeFuego += daño;
        }
    }
    
}