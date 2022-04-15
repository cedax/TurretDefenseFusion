using UnityEngine;

public class Balas : MonoBehaviour
{
    public float daño = 5.0f;

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Enemigo"){
            Destroy(gameObject);
        }
    }
}
