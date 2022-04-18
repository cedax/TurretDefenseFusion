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
        if(vida.VidaActual <= 0){
            _particulasDeMuerte.Play();
            GameObject meshObject = transform.parent.gameObject.transform.parent.gameObject;
            meshObject.SetActive(false);
            GameObject Enemy = transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;
            GameObject Canvas = Enemy.transform.GetChild(1).gameObject;
            Canvas.SetActive(false);
            Invoke("DestruirEnemigo", 1.2f);
        }
    }

    private void DestruirEnemigo(){
        vida.DestruirEnemigo();
    }
    
}
