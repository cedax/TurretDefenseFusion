using UnityEngine;

public class StatsTorreta : MonoBehaviour {
    [SerializeField] private float vidaInicial;
    [SerializeField] private float nivel;
    public float daño;
    private float vidaActual;
    private float costoSiguienteNivel;

    public int indexTOrreta { get; set; }

    private void Awake(){
        vidaActual = vidaInicial;
    }
    
}