using UnityEngine;

public class StatsTorreta : MonoBehaviour {
    [SerializeField] private float vidaInicial;
    [SerializeField] private int nivel;
    [SerializeField] private float dañoDeBalas;
    public int indexTOrreta { get; private set; }
    public float daño { get; set; }
    public float vidaActual { get; set; }
    public float costoSiguienteNivel { get; set; }
    public int Nivel => nivel;

    private void Awake(){
        vidaActual = vidaInicial;
        daño = dañoDeBalas;
    }

    private void Update() {
        if (daño != dañoDeBalas) {
            daño = dañoDeBalas;
        }
    }
    
}