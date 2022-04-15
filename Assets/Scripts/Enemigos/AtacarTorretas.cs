using UnityEngine;

public class AtacarTorretas : MonoBehaviour {
    [SerializeField] private float velocidad;
    private int indexTorretaObjetivo;
    private GameObject torretaObjetivo;

    private void Start() {
        ObtenerTorretaObjetivo();
    }

    private void ObtenerTorretaObjetivo(){
        indexTorretaObjetivo = Random.Range(0, ControlTorretas.Instancia.Torretas.Length);
        if(ControlTorretas.Instancia.Torretas[indexTorretaObjetivo] != null){
            torretaObjetivo = ControlTorretas.Instancia.Torretas[indexTorretaObjetivo];
        }else{
            ObtenerTorretaObjetivo();
        }
    }

    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(torretaObjetivo.transform.position.x, 1, torretaObjetivo.transform.position.z), velocidad);
    }
}