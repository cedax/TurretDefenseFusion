using UnityEngine;

public class ManagerJefe : MonoBehaviour {
    public float vidaBase;
    public float vidaActual;
    [SerializeField] private ParticleSystem particulasMuerte;

    private void Start() {
        BalanceoJuego.Instancia.JefesSuperados++;
        vidaBase = vidaBase*BalanceoJuego.Instancia.JefesSuperados;
        vidaActual = vidaBase;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Bala"){
            BalanceoJuego.Instancia.dañoHechoPorSegundo += other.gameObject.GetComponent<Balas>().daño;
            vidaActual -= other.gameObject.GetComponent<Balas>().daño;
            Jefe.Instancia.updateBarraVida(vidaActual, vidaBase);
        }
    }
    
    private void Update() {
        if(vidaActual <= 0){
            ParticleSystem particulasMuerteInstanciada = Instantiate(particulasMuerte, transform.position, Quaternion.identity);
            particulasMuerteInstanciada.Play();
            Destroy(particulasMuerteInstanciada.gameObject, particulasMuerteInstanciada.main.duration);
            Jefe.Instancia.subirLuces();
            GameObject padre = transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;
            Destroy(padre);
            Jefe.Instancia.jefe = null;
        }
    }
}