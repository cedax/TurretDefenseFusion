using UnityEngine;

public class DetectarClick : MonoBehaviour
{
    [SerializeField] private GameObject torretaPrefab;

    void Update(){
        if(ControlMenu.Instancia.Pausa == true) { return; }

        if (Input.GetMouseButtonDown(0)){
            IntantiateOnPosition(Input.mousePosition);
        }

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
            int index;
            try{
                index = int.Parse(info.collider.name);
            }catch{ return; }

            if(info.collider.tag == "Torreta"){
                if(ControlTorretas.Instancia.fusionandoTorretas == true) {
                    Debug.Log("Fusionando la torreta " + ControlTorretas.Instancia.torretaEmisora + " con la torreta " + index);
                    
                    StatsTorreta statsTorretaEmisora = ControlTorretas.Instancia.Torretas[ControlTorretas.Instancia.torretaEmisora].transform.GetChild(1).gameObject.GetComponent<StatsTorreta>();
                    StatsTorreta statsTorretaReceptora = ControlTorretas.Instancia.Torretas[index].transform.GetChild(1).gameObject.GetComponent<StatsTorreta>();

                    int nuevoNivelTorreta = statsTorretaEmisora.Nivel + statsTorretaReceptora.Nivel;

                    Destroy(ControlTorretas.Instancia.Torretas[ControlTorretas.Instancia.torretaEmisora]);

                    ControlTorretas.Instancia.desiluminarTorretas();
                    ControlTorretas.Instancia.fusionandoTorretas = false;
                }else {
                    ControlTorretas.Instancia.torretaEmisora = index;
                    ControlTorretas.Instancia.fusionandoTorretas = true;
                    StatsTorreta statsTorreta = ControlTorretas.Instancia.Torretas[index].transform.GetChild(1).gameObject.GetComponent<StatsTorreta>();
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
                }
            }
        }
    }
}