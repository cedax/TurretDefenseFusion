using UnityEngine;

public class Rotacion : MonoBehaviour
{
    [SerializeField] private float grados;

    public void RotarTorreta(GameObject _target){
        if(_target != null){
            Vector3 objetivoRotation = _target.transform.position - transform.position;
            objetivoRotation = new Vector3(objetivoRotation.x, objetivoRotation.y+2f, objetivoRotation.z);
            Debug.DrawRay(transform.position, objetivoRotation, Color.green);

            Quaternion ObjetivoDireccionQuaternion = Quaternion.LookRotation(objetivoRotation);

            Vector3 ObjetivoDireccionVector3 = ObjetivoDireccionQuaternion.eulerAngles;
            ObjetivoDireccionVector3.x = 0;
            ObjetivoDireccionVector3.y -= grados;
            ObjetivoDireccionVector3.z = 0;
            Quaternion ObjetivoDireccionQuaternion2 = Quaternion.Euler(ObjetivoDireccionVector3);
            transform.rotation = Quaternion.Slerp(transform.rotation, ObjetivoDireccionQuaternion2, Time.deltaTime * 5);
        }
    }
}
