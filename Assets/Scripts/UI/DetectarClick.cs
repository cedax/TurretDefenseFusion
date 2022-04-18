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
            if(info.collider.tag == "espacioTorreta"){
                int index = int.Parse(info.collider.name);
                if(ControlTorretas.Instancia.Torretas[index] != null){
                    Debug.Log(ControlTorretas.Instancia.Torretas[index].transform.position);
                }else {
                    float offset = 0f;
                    if(index <= 4){ offset = 0.31909786f; }
                    GameObject torreta = Instantiate(torretaPrefab, new Vector3(info.collider.transform.position.x+offset, info.collider.transform.position.y-2f, info.collider.transform.position.z-0.40f), Quaternion.identity);
                    ControlTorretas.Instancia.Torretas[index] = torreta;
                }
            }
        }
    }
}