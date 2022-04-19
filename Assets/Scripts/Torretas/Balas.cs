using UnityEngine;

public class Balas : MonoBehaviour
{
    public float daño = 5.0f;

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Enemigo"){
            Destroy(transform.parent.gameObject);
        }
    }
}
