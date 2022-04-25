using UnityEngine;

public class PowerUpVelocidadTorreta : MonoBehaviour {
    public void AumentarVelocidadDeTorretas(){
        BalanceoJuego.Instancia.multiplicadorVelocidadDisparo = 0.13f;
        GameObject padre = transform.parent.gameObject;
        Destroy(padre);
    }
}