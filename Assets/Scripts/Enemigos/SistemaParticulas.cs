using UnityEngine;

public class SistemaParticulas : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particulasDeMuerte;
    [SerializeField] private GameObject powerUpVida;
    [SerializeField] private GameObject powerUpVelocidad;
    private Vida vida;

    private void Awake() {
        vida = GetComponent<Vida>();
        _particulasDeMuerte.Stop();
    }

    private void FixedUpdate() {
        if(vida != null) {
            if(vida.VidaActual <= 0){
                if(BalanceoJuego.Instancia.Particulas){
                    ParticleSystem explosion = Instantiate(_particulasDeMuerte, transform.position, Quaternion.identity);
                    Destroy(explosion.gameObject, explosion.main.duration);
                }
                Destroy(transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject);
                Economia.Instancia.AgregarMonedas((int)vida.VidaInicial/2);
                if(Random.Range(0, 1000) < 15) {
                    if(Random.Range(0, 100) < 50) {
                        Instantiate(powerUpVida, transform.position, Quaternion.identity);
                    }else{
                        Instantiate(powerUpVelocidad, transform.position, Quaternion.identity);
                    }
                }
            }
        }
    }
}