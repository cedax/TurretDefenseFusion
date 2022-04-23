using UnityEngine;
using TMPro;

public class SistemaSpawn : Singleton<SistemaSpawn>
{
    [Header("Configuracion")]
    [SerializeField] private GameObject areaSpawn;
    [SerializeField] private GameObject[] modelosEnemigos;
    [SerializeField] private TextMeshProUGUI textoOleada;
    [SerializeField] private GameObject spawnJefe;
    [Header("Parametros")]
    public int cantidadEnemigos;
    private bool peleaConJefe;
    public int Oleada {get; private set;}
    private bool oleadaEnCurso;
    bool puedeSpawnear;
    bool primeraOleadaEjecutada;

    private void Start()
    {
        Oleada = 1;
        oleadaEnCurso = false;
        puedeSpawnear = false;
        textoOleada.text = Oleada.ToString();
        primeraOleadaEjecutada = false;
        peleaConJefe = false;
    }

    private void Update() {
        if(ControlEnemigos.Instancia.Enemigos.Count == 0 && primeraOleadaEjecutada && !peleaConJefe){
            oleadaEnCurso = false;
            puedeSpawnear = false;
            Oleada++;
            textoOleada.text = Oleada.ToString();
            BalanceoJuego.Instancia.multiplicadorDeVelocidad += 0.001f;
        }

        if(!puedeSpawnear){
            for (int i = 0; i < ControlTorretas.Instancia.Torretas.Length; i++){
                if(ControlTorretas.Instancia.Torretas[i] != null){
                    puedeSpawnear = true;
                }
            }
        }

        if(puedeSpawnear && !oleadaEnCurso){
            //if (Oleada % 10 == 0 && !oleadaEnCurso) {
            if (Oleada == 2 && !oleadaEnCurso) {
                oleadaEnCurso = true;
                puedeSpawnear = true;
                peleaConJefe = true;
                GameObject enemigo = Instantiate(modelosEnemigos[3], spawnJefe.transform.position, Quaternion.identity);
                enemigo.transform.Rotate(new Vector3(0, 90, 0));
                
                Jefe.Instancia.comenzarPelea();
            }else{
                int cantidadEnemigosCalculado = BalanceoJuego.Instancia.enemigosBase*Oleada;
                cantidadEnemigos = Random.Range(cantidadEnemigosCalculado, cantidadEnemigosCalculado+2);
                oleadaEnCurso = true;
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        if(!primeraOleadaEjecutada){
            primeraOleadaEjecutada = true;
        }

        int rangoMaximo = 0;
        if(Oleada <= 10){
            rangoMaximo = 1;
        }else if(Oleada >= 11 && Oleada <= 20){
            rangoMaximo = 2;
        }

        for (int i = 0; i < cantidadEnemigos; ++i){
            Vector3 posicion = new Vector3(Random.Range(areaSpawn.transform.position.x - areaSpawn.transform.localScale.x / 2, areaSpawn.transform.position.x + areaSpawn.transform.localScale.x / 2),
                                            areaSpawn.transform.position.y,
                                            Random.Range(areaSpawn.transform.position.z - areaSpawn.transform.localScale.z / 2, areaSpawn.transform.position.z + areaSpawn.transform.localScale.z / 2));
            GameObject enemigo = Instantiate(modelosEnemigos[Random.Range(0, rangoMaximo)], posicion, Quaternion.identity);
            enemigo.transform.Rotate(new Vector3(0, 90, 0));
            ControlEnemigos.Instancia.Enemigos.Add(enemigo);
        }
    }
}