using UnityEngine;

public class Vida : MonoBehaviour {
    [SerializeField] private float vidaInicial;
    private float vidaActual;

    private void Start() {
        vidaActual = vidaInicial;
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Bala"){
            vidaActual -= collision.gameObject.GetComponent<Balas>().daño;
        }
    }
}