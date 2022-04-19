using UnityEngine;

public class ControlTorretas : Singleton<ControlTorretas>
{
    [SerializeField] private GameObject[] torretas;
    [SerializeField] private GameObject seleccionarTorretaPrefab;
    [SerializeField] private GameObject espaciosTorretas;
    public GameObject[] Torretas => torretas;
    public GameObject[] torretasSeleccionadas;
    public int Dificultad { get; private set; }
    public int torretaEmisora;
    public int torretaReceptora;
    public bool fusionandoTorretas;

    override protected void Awake() {
        base.Awake();
        fusionandoTorretas = false;
    }

    private void Start() {
        torretas = new GameObject[10];
        torretasSeleccionadas = new GameObject[10];
    }

    public void iluminarTorretas(int nivel){
        for (var i = 0; i < Torretas.Length; i++){
            if(Torretas[i] != null){
                StatsTorreta statsTorreta = Torretas[i].transform.GetChild(1).gameObject.GetComponent<StatsTorreta>();
                if(statsTorreta.Nivel == nivel && espaciosTorretas.transform.GetChild(i).transform.childCount == 1){
                    GameObject torreta = Instantiate(seleccionarTorretaPrefab, torretas[i].gameObject.transform.position, Quaternion.identity);
                    torreta.transform.parent = espaciosTorretas.transform.GetChild(i);
                    torreta.transform.localPosition = Vector3.zero;
                    torreta.transform.localScale = new Vector3(4.5f, 2.5f, 4.5f);
                    ControlTorretas.Instancia.torretasSeleccionadas[i] = torreta;
                    
                }
            }
        }
    }

    public void desiluminarTorretas(){
        for (var i = 0; i < torretasSeleccionadas.Length; i++){
            if(torretasSeleccionadas[i] != null){
                Destroy(torretasSeleccionadas[i]);
            }
        }
    }
}
