using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour {
    [SerializeField] private float vidaInicial;
    [SerializeField] private Image barraVida;
    private float vidaActual;
    public float VidaActual => vidaActual;

    private void Start() {
        vidaActual = vidaInicial;
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Bala"){
            vidaActual -= other.gameObject.GetComponent<Balas>().daño;
            barraVida.fillAmount = Mathf.Lerp(barraVida.fillAmount, (vidaActual/vidaInicial)-0.2f, Time.deltaTime * 10f);
        }
    }
}