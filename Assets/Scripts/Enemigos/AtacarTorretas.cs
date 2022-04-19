using UnityEngine;

public class AtacarTorretas : MonoBehaviour {
    [SerializeField] private float velocidad;
    [SerializeField] private ParticleSystem particulasDeExplosion;
    private int indexTorretaObjetivo;
    private GameObject torretaObjetivo;


    private void Start(){
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

    private void FixedUpdate(){
        try{
            float distancia = Vector3.Distance(transform.position, torretaObjetivo.transform.position);
            if(distancia < 5f){
                ParticleSystem explosion = Instantiate(particulasDeExplosion, transform.position, Quaternion.identity);
                explosion.transform.position = new Vector3(explosion.transform.position.x + 2.626035f, explosion.transform.position.y, explosion.transform.position.z);
                Destroy(explosion.gameObject, explosion.main.duration);
                Destroy(gameObject);
            }else {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(torretaObjetivo.transform.position.x, 1, torretaObjetivo.transform.position.z), velocidad);
                Quaternion lookOnLook = Quaternion.LookRotation(torretaObjetivo.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, 10f*Time.deltaTime);
            }
        }catch{
            ObtenerTorretaObjetivo();
        }
    }
}