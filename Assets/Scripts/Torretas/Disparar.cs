using UnityEngine;

public class Disparar : MonoBehaviour
{
    [SerializeField] private GameObject _bala;
    [SerializeField] [Range(0, 7500)] private int fuerzaBala = 3000;
    [SerializeField] [Range(0.1f, 10)] private float velocidad = 1;
    [SerializeField] private float grados;
    private float VelocidadAnterior = 1;
    private GameObject _target;
    public GameObject Target => _target;
    private Rotacion rotacion;
    private AudioTorreta audioTorreta;

    private void Start() {
        InvokeRepeating("DispararBala", 0, velocidad);
    }

    private void Awake() {
        rotacion = GetComponent<Rotacion>();
        audioTorreta = GetComponent<AudioTorreta>();
    }

    private void Update() {
        if(velocidad != VelocidadAnterior){
            CancelInvoke("DispararBala");
            InvokeRepeating("DispararBala", 0, velocidad);
            VelocidadAnterior = velocidad;
        }

        if(SistemaSpawn.Instancia.peleaConJefe){
            try{
                _target = Jefe.Instancia.jefe.gameObject;
            }catch{}
        }
        
        // Nota Quitar: Dejar el _Target como blanco cuando se elimine el Jefe
        if(_target == null){ ElegirEnemigo(); }
        rotacion.RotarTorreta(_target);
    }

    private void ElegirEnemigo(){
        try{
            int index = Random.Range(0, ControlEnemigos.Instancia.Enemigos.Count);
            _target = ControlEnemigos.Instancia.Enemigos[index];
        }
        catch (System.Exception){
            // Nota: Aun no hay enemigos para atacar
        }
    }

    private void DispararBala(){
        if(_target != null) {
            if(SistemaSpawn.Instancia.peleaConJefe && !Jefe.Instancia.LucesListas){return;}
            Vector3 objetivoRotation = new Vector3(_target.transform.position.x, _target.transform.position.y + 2f, _target.transform.position.z) - transform.position;
            Quaternion ObjetivoDireccionQuaternion = Quaternion.LookRotation(objetivoRotation);
            GameObject bala = Instantiate(_bala, transform.position, ObjetivoDireccionQuaternion);
            
            Transform objHijoBala = bala.transform.GetChild(0);
            var angles = objHijoBala.rotation.eulerAngles;
            angles.y += grados;
            objHijoBala.rotation = Quaternion.Euler(angles);

            objHijoBala.GetComponent<Rigidbody>().AddForce(bala.transform.forward * fuerzaBala);

            audioTorreta.Disparo();

            objHijoBala.GetComponent<Balas>().daño = GetComponent<StatsTorreta>().daño;

            Destroy(bala, 5);
        }
    }

}
