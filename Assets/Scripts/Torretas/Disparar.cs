using UnityEngine;

public class Disparar : MonoBehaviour
{
    [SerializeField] private GameObject _bala;
    [SerializeField] [Range(0, 7500)] private int fuerzaBala = 3000;
    [SerializeField] [Range(0.1f, 10)] private float velocidad = 1;
    [SerializeField] private float grados;
    private float VelocidadAnterior;

    private void Start() {
        InvokeRepeating("DispararBala", 0, velocidad);
    }

    private void Update() {
        if(velocidad != VelocidadAnterior){
            CancelInvoke("DispararBala");
            InvokeRepeating("DispararBala", 0, velocidad);
            VelocidadAnterior = velocidad;
        }
    }

    private void DispararBala(){
        try{
            int index = Random.Range(0, ControlEnemigos.Instancia.Enemigos.Count);
            GameObject _target = ControlEnemigos.Instancia.Enemigos[index];

            Vector3 objetivoRotation = new Vector3(_target.transform.position.x, _target.transform.position.y + 2f, _target.transform.position.z) - transform.position;
            Quaternion ObjetivoDireccionQuaternion = Quaternion.LookRotation(objetivoRotation);
            GameObject bala = Instantiate(_bala, transform.position, ObjetivoDireccionQuaternion);
            
            Transform objHijoBala = bala.transform.GetChild(0);
            var angles = objHijoBala.rotation.eulerAngles;
            angles.y += grados;
            objHijoBala.rotation = Quaternion.Euler(angles);

            bala.GetComponent<Rigidbody>().AddForce(bala.transform.forward * fuerzaBala);
            Destroy(bala, 5);
        }
        catch (System.Exception ex){
            Debug.Log(ex.Message);
            Debug.Log("Aun no hay enemigos para atacar");
        }
    }

}
