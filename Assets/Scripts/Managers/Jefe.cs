using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Jefe : Singleton<Jefe>{
    [SerializeField] private Light luz;
    [SerializeField] private Camera camara;
    [SerializeField] private GameObject misil;
    public GameObject spawnJefe;
    public bool LucesListas;
    public GameObject jefe;
    public Image barraVida;
    public Image barraVidaFondo;
    public Image textoBoss;
    private float duration = 4f;
    private float magnitude = 0.20f;
    [SerializeField] private TextMeshProUGUI oleadasRestantes;

    public int contadorProximaOleada;
    private int contadorProximaOleadaTemp;
    private void Update() {
        if(contadorProximaOleada != contadorProximaOleadaTemp){
            contadorProximaOleadaTemp = contadorProximaOleada;
            oleadasRestantes.text = contadorProximaOleadaTemp.ToString();
        }
    }

    protected override void Awake() {
        LucesListas = false;
    }

    private void Start() {
        barraVida.enabled = false;
        barraVidaFondo.enabled = false;
        textoBoss.enabled = false;
        contadorProximaOleadaTemp = contadorProximaOleada;
        oleadasRestantes.text = contadorProximaOleada.ToString();
    }

    public void updateBarraVida(float vidaActual, float vidaInicial){
        barraVida.fillAmount = Mathf.Lerp(barraVida.fillAmount, (vidaActual/vidaInicial)-0.05f, Time.deltaTime * 10f);
    }

    public void comenzarPelea(){
        StartCoroutine(Shake());
        Vibrar.Vibrate((long)((duration-2f)*1000));
        InvokeRepeating("bajarLuces", 0, 0.3f);
        InvokeRepeating("DispararMisil", 0, 2f+BalanceoJuego.Instancia.JefesSuperados/10);
        barraVida.enabled = true;
        barraVidaFondo.enabled = true;
        textoBoss.enabled = true;
    }

    private void OnEnable() {
        SistemaSpawn.Instancia.EventoComenzarPelea += comenzarPelea;
    }

    private void OnDisable() {
        SistemaSpawn.Instancia.EventoComenzarPelea -= comenzarPelea;
    }

    private void bajarLuces(){
        if(LucesListas){return;}
        if(luz.intensity > 0.3f){
            luz.intensity -= 0.1f;
        }else{
            LucesListas = true;
        }
    }

    private void SubirLuces(){
        if(luz.intensity < 1){
            luz.intensity += 0.1f;
        }else{
            CancelInvoke("SubirLuces");
            LucesListas = false;
            SistemaSpawn.Instancia.puedeSpawnear = true;
            SistemaSpawn.Instancia.peleaConJefe = false;
            SistemaSpawn.Instancia.oleadaEnCurso = false;
            Jefe.Instancia.contadorProximaOleada = 5;
            if (AnuncioBonificado.Instancia.interstitial.IsLoaded()) {
                Debug.Log("Anuncio cargado");
                AnuncioBonificado.Instancia.interstitial.Show();
            }
        }
    }

    public void subirLuces(){
        barraVida.fillAmount = 1;
        barraVida.enabled = false;
        barraVidaFondo.enabled = false;
        textoBoss.enabled = false;
        CancelInvoke("bajarLuces");
        CancelInvoke("DispararMisil");
        InvokeRepeating("SubirLuces", 0, 0.3f);
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
        GameObject misilJefe = Instantiate(misil, new Vector3(spawnJefe.transform.position.x, spawnJefe.transform.position.y+4.5f, spawnJefe.transform.position.z), Quaternion.identity);
        ObtenerTorretaObjetivo();
        misilJefe.GetComponent<Rigidbody>().AddForce((torretaObjetivo.transform.position - spawnJefe.transform.position)*2f, ForceMode.Impulse);
        Vector3 objetivoRotation = new Vector3(torretaObjetivo.transform.position.x, torretaObjetivo.transform.position.y, torretaObjetivo.transform.position.z) - misilJefe.transform.position;
        Quaternion ObjetivoDireccionQuaternion = Quaternion.LookRotation(objetivoRotation);
        misilJefe.transform.rotation = ObjetivoDireccionQuaternion;
        misilJefe.transform.Rotate(0, 90, 0);
    }
}