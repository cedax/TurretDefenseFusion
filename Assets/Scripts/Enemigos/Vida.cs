using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour {
    [SerializeField] private float vidaInicial;
    [SerializeField] private Image barraVida;
    private float vidaActual;
    public float VidaActual => vidaActual;
    public float VidaInicial => vidaInicial;

    private void Start() {
        vidaActual = vidaInicial;
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Bala"){
            BalanceoJuego.Instancia.dañoHechoPorSegundo += other.gameObject.GetComponent<Balas>().daño;
            vidaActual -= other.gameObject.GetComponent<Balas>().daño;
            barraVida.fillAmount = Mathf.Lerp(barraVida.fillAmount, (vidaActual/vidaInicial)-0.2f, Time.deltaTime * 10f);
        }
    }
}