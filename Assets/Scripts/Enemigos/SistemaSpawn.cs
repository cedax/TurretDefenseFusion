using UnityEngine;
using TMPro;
using System;

public class SistemaSpawn : Singleton<SistemaSpawn>
{
    [Header("Configuracion")]
    [SerializeField] private GameObject areaSpawn;
    [SerializeField] private GameObject[] modelosEnemigos;
    [SerializeField] private TextMeshProUGUI textoOleada;
    [SerializeField] private GameObject spawnJefe;
    [SerializeField] private ParticleSystem spawnJefeParticulas;
    [Header("Parametros")]
    public Action EventoComenzarPelea;
    public int cantidadEnemigos;
    public bool peleaConJefe;
    public int Oleada {get; private set;}
    public bool oleadaEnCurso;
    public bool puedeSpawnear;
    bool primeraOleadaEjecutada;

    private void Start()
    {
        Oleada = 1;
        Jefe.Instancia.contadorProximaOleada = 4;
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
            Jefe.Instancia.contadorProximaOleada --;
            textoOleada.text = Oleada.ToString();
            BalanceoJuego.Instancia.multiplicadorDeVelocidad += 0.0005f;
        }

        if(!puedeSpawnear){
            for (int i = 0; i < ControlTorretas.Instancia.Torretas.Length; i++){
                if(ControlTorretas.Instancia.Torretas[i] != null){
                    puedeSpawnear = true;
                }
            }
        }

        if(puedeSpawnear && !oleadaEnCurso){
            if (Oleada % 5 == 0 && !oleadaEnCurso) {
                oleadaEnCurso = true;
                puedeSpawnear = false;
                peleaConJefe = true;

                GameObject enemigo = Instantiate(modelosEnemigos[3], spawnJefe.transform.position, Quaternion.identity);
                enemigo.transform.Rotate(new Vector3(0, 90, 0));
                ParticleSystem particulasSpawn = Instantiate(spawnJefeParticulas, spawnJefe.transform.position, Quaternion.identity);
                spawnJefeParticulas.Play();
                Destroy(particulasSpawn.gameObject, particulasSpawn.main.duration);
                Jefe.Instancia.jefe = enemigo;
                EventoComenzarPelea?.Invoke(); 
            }else if(!peleaConJefe){
                int cantidadEnemigosCalculado = BalanceoJuego.Instancia.enemigosBase*Oleada;
                cantidadEnemigos = UnityEngine.Random.Range(cantidadEnemigosCalculado, cantidadEnemigosCalculado+2);
                oleadaEnCurso = true;
                puedeSpawnear = false;
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
            Vector3 posicion = new Vector3(UnityEngine.Random.Range(areaSpawn.transform.position.x - areaSpawn.transform.localScale.x / 2, areaSpawn.transform.position.x + areaSpawn.transform.localScale.x / 2),
                                            areaSpawn.transform.position.y,
                                            UnityEngine.Random.Range(areaSpawn.transform.position.z - areaSpawn.transform.localScale.z / 2, areaSpawn.transform.position.z + areaSpawn.transform.localScale.z / 2));
            GameObject enemigo = Instantiate(modelosEnemigos[UnityEngine.Random.Range(0, rangoMaximo)], posicion, Quaternion.identity);
            enemigo.transform.Rotate(new Vector3(0, 90, 0));
            ControlEnemigos.Instancia.Enemigos.Add(enemigo);
        }
    }
}