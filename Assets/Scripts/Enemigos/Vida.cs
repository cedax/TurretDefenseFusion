using UnityEngine;

public class Vida : MonoBehaviour {
    [SerializeField] private float vidaInicial;
    [SerializeField] private RectTransform barraVida;
    private float vidaActual;
    public float VidaActual => vidaActual;
    private void Start() {
        vidaActual = vidaInicial;
    }

    public void DestruirEnemigo(){
        Destroy(transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject);
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Bala"){
            vidaActual -= other.gameObject.GetComponent<Balas>().daño;
            barraVida.sizeDelta = new Vector2(vidaActual / vidaInicial * barraVida.sizeDelta.x , barraVida.sizeDelta.y);
        }
    }
}