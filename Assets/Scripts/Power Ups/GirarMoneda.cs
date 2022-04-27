using UnityEngine;

public class GirarMoneda : MonoBehaviour
{
    void Update(){
        transform.Rotate(new Vector3(0, -1, 0));
        if(Vector3.Distance(transform.position, TorretaAsistidaGame.Instancia.FocusMonedas.transform.position) <= 0.5f){
            Destroy(gameObject);
        }
    }

    private void MoverItem(){
        transform.position = Vector3.MoveTowards(transform.position, TorretaAsistidaGame.Instancia.FocusMonedas.transform.position, 0.5f);
    }

    private void Start() {
        InvokeRepeating("MoverItem", 0f, 0.3f);
        Debug.Log("GirarMoneda");
    }
}
