using UnityEngine;

public class Disparar : MonoBehaviour
{
    [SerializeField] private GameObject _bala;
    [SerializeField] [Range(0, 7500)] private int fuerzaBala = 3000;
    [SerializeField] [Range(0.1f, 10)] private float velocidad = 1;
    [SerializeField] private float grados;
    private float VelocidadAnterior = 1;
    private GameObject _target;
    public GameObject ObjetivoTorretaAsistida { get; set; }
    private Rotacion rotacion;
    private AudioTorreta audioTorreta;

    private TorretaAsistida torretaAsistida;
    private bool torretaAsistidaBol;
    private float multiplicadorVelocidadDisparoTemp = 0;

    private void Start() {
        InvokeRepeating("DispararBala", 0, velocidad);
        
        torretaAsistida = GetComponent<TorretaAsistida>();
        multiplicadorVelocidadDisparoTemp = BalanceoJuego.Instancia.multiplicadorVelocidadDisparo;

        if(torretaAsistida == null){
            torretaAsistidaBol = false;
        }else{
            torretaAsistidaBol = true;
        }
    }

    private void Awake() {
        torretaAsistidaBol = false;
        rotacion = GetComponent<Rotacion>();
        audioTorreta = GetComponent<AudioTorreta>();
    }

    private void RestaurarVelocidad(){
        velocidad = 0.8f;
        BalanceoJuego.Instancia.multiplicadorVelocidadDisparo = 0f;
        multiplicadorVelocidadDisparoTemp = 0f;
    }

    private void Update() {
        if(multiplicadorVelocidadDisparoTemp != BalanceoJuego.Instancia.multiplicadorVelocidadDisparo){
            float comprobar = velocidad * BalanceoJuego.Instancia.multiplicadorVelocidadDisparo;
            if(comprobar != 0){
                velocidad = comprobar;
                Invoke("RestaurarVelocidad", 15f);
            }else{
                velocidad = 0.8f;
            }
            multiplicadorVelocidadDisparoTemp = BalanceoJuego.Instancia.multiplicadorVelocidadDisparo;
        }

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
        
        if(!torretaAsistidaBol){
            if(_target == null){ ElegirEnemigo(); }
            rotacion.RotarTorreta(_target);
        }

        if(ObjetivoTorretaAsistida != null){
            rotacion.RotarTorreta(ObjetivoTorretaAsistida);
        }
    }

    private void ElegirEnemigo(){
        try{
            int index = Random.Range(0, ControlEnemigos.Instancia.Enemigos.Count);
            _target = ControlEnemigos.Instancia.Enemigos[index];
        }
        catch{}
    }

    private void DispararBala(){
        if(_target != null && !torretaAsistidaBol) {
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

    public void DispararBalaTorretaAsistida(){
        if(ObjetivoTorretaAsistida != null && torretaAsistida.puedeDisparar){
            Vector3 objetivoRotation = new Vector3(ObjetivoTorretaAsistida.transform.position.x, ObjetivoTorretaAsistida.transform.position.y, ObjetivoTorretaAsistida.transform.position.z) - transform.position;
            Quaternion ObjetivoDireccionQuaternion = Quaternion.LookRotation(objetivoRotation);
            GameObject bala = Instantiate(_bala, transform.position, ObjetivoDireccionQuaternion);
            Transform objHijoBala = bala.transform.GetChild(0);
            var angles = objHijoBala.rotation.eulerAngles;
            angles.y += grados;
            objHijoBala.rotation = Quaternion.Euler(angles);
            objHijoBala.GetComponent<Rigidbody>().AddForce(bala.transform.forward * fuerzaBala);
            audioTorreta.Disparo();
            objHijoBala.GetComponent<Balas>().daño = GetComponent<StatsTorreta>().daño;
            torretaAsistida.balasDisparadas++;
            Destroy(bala, 5);
        }
    }

}
