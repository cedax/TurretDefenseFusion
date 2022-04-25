using UnityEngine;

public class TorretaAsistida : MonoBehaviour {
    public int balasDisparadas;
    public bool puedeDisparar;

    public GameObject torretaFria;
    public GameObject torretaCaliente;
    public GameObject torretaTibia;

    private void Start() {
        puedeDisparar = true;
    }

    private void Update() {
        if(balasDisparadas <= 10){
            torretaFria.SetActive(true);
            torretaTibia.SetActive(false);
            torretaCaliente.SetActive(false);
        }else if(balasDisparadas > 10 && balasDisparadas < 20){
            torretaFria.SetActive(false);
            torretaTibia.SetActive(true);
            torretaCaliente.SetActive(false);
        }else if(balasDisparadas >= 20){
            torretaFria.SetActive(false);
            torretaTibia.SetActive(false);
            torretaCaliente.SetActive(true);
        }

        if(balasDisparadas >= 20){
            puedeDisparar = false;
            Invoke("ActivarDisparo", 10f);
        }
    }

    private void ActivarDisparo(){
        puedeDisparar = true;
        balasDisparadas = 0;
    }
}