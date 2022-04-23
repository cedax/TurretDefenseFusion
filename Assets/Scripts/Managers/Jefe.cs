using UnityEngine;
using System.Collections;

public class Jefe : Singleton<Jefe>{
    [SerializeField] private Light luz;
    [SerializeField] private Camera camara;
    [SerializeField] private GameObject misil;
    [SerializeField] private GameObject jefe;
    [SerializeField] private Transform[] puntosDeControlMisil;

    private float duration = 4f;
    private float magnitude = 0.20f;
    private bool LucesListas;

    protected override void Awake() {
        base.Awake();
        LucesListas = false;
    }

    public void comenzarPelea(){
        StartCoroutine(Shake());
        Vibrar.Vibrate((long)duration*1000);
        InvokeRepeating("bajarLuces", 0, 0.5f);
        InvokeRepeating("DispararMisil", 0, 2);
    }

    private void bajarLuces(){
        if(LucesListas){return;}
        if(luz.intensity > 0.3f){
            luz.intensity -= 0.1f;
        }else{
            LucesListas = true;
        }
    }

    private IEnumerator Shake(){
        Vector3 originalPos = camara.transform.position;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            camara.transform.position = new Vector3(originalPos.x+x, originalPos.y+y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;
    }

    private GameObject torretaObjetivo;
    private void ObtenerTorretaObjetivo(){
        int indexTorretaObjetivo = Random.Range(0, ControlTorretas.Instancia.Torretas.Length);
        if(ControlTorretas.Instancia.Torretas[indexTorretaObjetivo] != null){
            torretaObjetivo = ControlTorretas.Instancia.Torretas[indexTorretaObjetivo];
        }else{
            ObtenerTorretaObjetivo();
        }
    }

    private void DispararMisil(){
        if(!LucesListas) { return; }
        GameObject misilJefe = Instantiate(misil, new Vector3(jefe.transform.position.x, jefe.transform.position.y+4.5f, jefe.transform.position.z), Quaternion.identity);
        ObtenerTorretaObjetivo();
        misilJefe.GetComponent<Rigidbody>().AddForce((torretaObjetivo.transform.position - jefe.transform.position)*2f, ForceMode.Impulse);
        Vector3 objetivoRotation = new Vector3(torretaObjetivo.transform.position.x, torretaObjetivo.transform.position.y, torretaObjetivo.transform.position.z) - misilJefe.transform.position;
        Quaternion ObjetivoDireccionQuaternion = Quaternion.LookRotation(objetivoRotation);
        misilJefe.transform.rotation = ObjetivoDireccionQuaternion;
        misilJefe.transform.Rotate(0, 90, 0);
    }
}