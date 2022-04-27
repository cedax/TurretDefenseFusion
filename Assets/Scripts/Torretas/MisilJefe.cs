using UnityEngine;

public class MisilJefe : MonoBehaviour{
    [SerializeField] private ParticleSystem explosionMisil;

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Torreta"){
            if(BalanceoJuego.Instancia.Particulas){
                ParticleSystem particulasExplosion = Instantiate(explosionMisil, transform.position, Quaternion.identity);
                Destroy(particulasExplosion.gameObject, explosionMisil.main.duration);
            }
            Destroy(gameObject);
            ManagerVida.Instancia.RestarVida((int)(SistemaSpawn.Instancia.Oleada*BalanceoJuego.Instancia.multiplicadorDeDañoMisil));
        }
    }
}
