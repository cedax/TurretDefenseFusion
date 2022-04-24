using UnityEngine;

public class SistemaParticulas : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particulasDeMuerte;
    private Vida vida;

    private void Awake() {
        vida = GetComponent<Vida>();
        _particulasDeMuerte.Stop();
    }

    private void FixedUpdate() {
        if(vida != null) {
            if(vida.VidaActual <= 0){
                ParticleSystem explosion = Instantiate(_particulasDeMuerte, transform.position, Quaternion.identity);
                Destroy(explosion.gameObject, explosion.main.duration);
                Destroy(transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject);
                Economia.Instancia.AgregarMonedas((int)vida.VidaInicial/2);
            }
        }
    }
}