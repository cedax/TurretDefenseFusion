using UnityEngine;
using System.Collections.Generic;

public class SistemaSpawn : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private GameObject areaSpawn;
    [SerializeField] private GameObject[] modelosEnemigos;
    [Header("Parametros")]
    [SerializeField] [Range(0, 10)] private int cantidadEnemigos;
    [SerializeField] [Range(0, 30)] private int tiempoSpawn;
    private int tiempoSpawnAnterior;

    private void Start()
    {
        tiempoSpawnAnterior = tiempoSpawn;
        InvokeRepeating("Spawn", 0, tiempoSpawn);
    }

    private void Update() {
        if(tiempoSpawnAnterior != tiempoSpawn) {
            CancelInvoke("Spawn");
            tiempoSpawnAnterior = tiempoSpawn;
            InvokeRepeating("Spawn", tiempoSpawn, tiempoSpawn);
        }
    }

    private void Spawn()
    {
        bool spawn = false;
        for (int i = 0; i < ControlTorretas.Instancia.Torretas.Length; i++){
            if(ControlTorretas.Instancia.Torretas[i] != null){
                spawn = true;
            }
        }

        if(spawn){
            for (int i = 0; i < cantidadEnemigos; ++i){
                Vector3 posicion = new Vector3(Random.Range(areaSpawn.transform.position.x - areaSpawn.transform.localScale.x / 2, areaSpawn.transform.position.x + areaSpawn.transform.localScale.x / 2),
                                                areaSpawn.transform.position.y,
                                                Random.Range(areaSpawn.transform.position.z - areaSpawn.transform.localScale.z / 2, areaSpawn.transform.position.z + areaSpawn.transform.localScale.z / 2));
                GameObject enemigo = Instantiate(modelosEnemigos[Random.Range(0, modelosEnemigos.Length)], posicion, Quaternion.identity);
                ControlEnemigos.Instancia.Enemigos.Add(enemigo);
            }
        }
    }
}