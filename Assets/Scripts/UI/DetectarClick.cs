using UnityEngine;

public class DetectarClick : MonoBehaviour
{
    [SerializeField] private GameObject torretaPrefab;

    void Update(){
        if (Input.GetMouseButtonDown(0)){
            IntantiateOnPosition(Input.mousePosition);
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
                    GameObject torreta = Instantiate(torretaPrefab, new Vector3(info.collider.transform.position.x-1.11f, info.collider.transform.position.y, info.collider.transform.position.z+0.2181381f), Quaternion.identity);
                    ControlTorretas.Instancia.Torretas[index] = torreta;
                    Debug.Log(ControlTorretas.Instancia.Torretas[index].transform.position);
                }
            }
        }
    }
}