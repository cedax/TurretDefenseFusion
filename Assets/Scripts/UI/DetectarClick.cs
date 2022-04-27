using UnityEngine;

public class DetectarClick : MonoBehaviour
{
    [SerializeField] private GameObject torretaPrefab;

    [SerializeField] private GameObject[] torretas;
    [SerializeField] private GameObject _bala;

    void Update(){
        if(ControlMenu.Instancia.Pausa == true) { return; }

        /*
        if (Input.GetMouseButtonDown(0)){
            IntantiateOnPosition(Input.mousePosition);
        }
        */

        if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began){
                IntantiateOnPosition(touch.position);
            }
        }
    }

    void IntantiateOnPosition(Vector3 mousePos){
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if(Physics.Raycast(ray, out RaycastHit info)){
            if(info.collider.tag == "EnemigoBoxTap" || info.collider.tag == "Enemigo"){
                if(Economia.Instancia.Monedas > 1){
                    Economia.Instancia.RestarMonedas(1);
                    TorretaAsistidaGame.Instancia.TorretaGameObject.GetComponent<Disparar>().ObjetivoTorretaAsistida = info.collider.gameObject;
                    TorretaAsistidaGame.Instancia.TorretaGameObject.GetComponent<Disparar>().DispararBalaTorretaAsistida();
                }
            }

            if(info.collider.tag == "AumentarVelocidadTorretas"){
                info.collider.gameObject.GetComponent<PowerUpVelocidadTorreta>().AumentarVelocidadDeTorretas();
            }

            if(info.collider.tag == "CurarVida"){
                info.collider.gameObject.GetComponent<PowerUpCurar>().Curar();
            }

            int index;
            try{
                index = int.Parse(info.collider.name);
            }catch{ return; }

            if(info.collider.tag == "Torreta"){
                if(ControlTorretas.Instancia.fusionandoTorretas == true) {
                    if(ControlTorretas.Instancia.torretaEmisora == index) {
                        ControlTorretas.Instancia.fusionandoTorretas = false;
                        ControlTorretas.Instancia.desiluminarTorretas();
                        return;
                    }

                    StatsTorreta statsTorretaEmisora = ControlTorretas.Instancia.Torretas[ControlTorretas.Instancia.torretaEmisora].transform.GetChild(1).gameObject.GetComponent<StatsTorreta>();
                    StatsTorreta statsTorretaReceptora = ControlTorretas.Instancia.Torretas[index].transform.GetChild(1).gameObject.GetComponent<StatsTorreta>();
                    
                    if(statsTorretaEmisora.Nivel != statsTorretaReceptora.Nivel){
                        ControlTorretas.Instancia.fusionandoTorretas = false;
                        ControlTorretas.Instancia.desiluminarTorretas();
                        Notificacion.Instancia.Nueva("Solo puedes combinar torretas del mismo nivel");
                        return;
                    }

                    int costoTorreta = 25*SistemaSpawn.Instancia.Oleada*statsTorretaEmisora.Nivel;
                    if(Economia.Instancia.Balance() >= costoTorreta) {
                        Economia.Instancia.RestarMonedas(costoTorreta);
                    }else{
                        ControlTorretas.Instancia.fusionandoTorretas = false;
                        ControlTorretas.Instancia.desiluminarTorretas();
                        Notificacion.Instancia.Nueva("Necesitas " + (costoTorreta - Economia.Instancia.Balance()) + " monedas mas");
                        return;
                    }

                    Destroy(ControlTorretas.Instancia.Torretas[ControlTorretas.Instancia.torretaEmisora]);
                    Destroy(ControlTorretas.Instancia.Torretas[index]);
                    ControlTorretas.Instancia.desiluminarTorretas();
                    ControlTorretas.Instancia.fusionandoTorretas = false;
                    GameObject nuevaTorreta = Instantiate(torretas[statsTorretaReceptora.Nivel+1], ControlTorretas.Instancia.Torretas[index].transform.position, Quaternion.identity);
                    nuevaTorreta.name = index.ToString();
                    ControlTorretas.Instancia.Torretas[index] = nuevaTorreta;

                }else {
                    StatsTorreta statsTorreta = ControlTorretas.Instancia.Torretas[index].transform.GetChild(1).gameObject.GetComponent<StatsTorreta>();
                    if(statsTorreta.Nivel >= torretas.Length-1) {
                        Notificacion.Instancia.Nueva("El nivel maximo de torretas fue alcanzado");
                        return;
                    }
                    ControlTorretas.Instancia.torretaEmisora = index;
                    ControlTorretas.Instancia.fusionandoTorretas = true;
                    ControlTorretas.Instancia.iluminarTorretas(statsTorreta.Nivel);
                }
            }

            if(info.collider.tag == "espacioTorreta"){
                if(ControlTorretas.Instancia.Torretas[index] == null) {
                    float offset = 0f;
                    if(index <= 4){ offset = 0.31909786f; }
                    GameObject torreta = Instantiate(torretaPrefab, new Vector3(info.collider.transform.position.x+offset, info.collider.transform.position.y-2f, info.collider.transform.position.z-0.40f), Quaternion.identity);
                    torreta.name = index.ToString();
                    ControlTorretas.Instancia.Torretas[index] = torreta;
                    ControlTorretas.Instancia.desiluminarTorretas();
                    ControlTorretas.Instancia.fusionandoTorretas = false;
                    GameObject statsTorreta = ControlTorretas.Instancia.Torretas[index].transform.GetChild(1).gameObject;
                    int costoTorreta = statsTorreta.GetComponent<StatsTorreta>().Nivel * BalanceoJuego.Instancia.costoBaseTorreta;
                    if(BalanceoJuego.Instancia.multiplicadorVelocidadDisparo != 0){
                        statsTorreta.GetComponent<Disparar>().velocidad = statsTorreta.GetComponent<Disparar>().velocidad * BalanceoJuego.Instancia.multiplicadorVelocidadDisparo;
                    }
                    if(Economia.Instancia.Balance() >= costoTorreta) {
                        Economia.Instancia.RestarMonedas(costoTorreta);
                    }else{
                        Destroy(ControlTorretas.Instancia.Torretas[index]);
                        Notificacion.Instancia.Nueva("Nececitas " + (costoTorreta - Economia.Instancia.Balance()) + " monedas mas");
                    }
                }
            }
        }
    }
}