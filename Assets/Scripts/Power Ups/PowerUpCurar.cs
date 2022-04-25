using UnityEngine;

public class PowerUpCurar : MonoBehaviour {
    public void Curar(){
        ManagerVida.Instancia.SumarVida(100);
        GameObject padre = transform.parent.gameObject;
        Destroy(padre);
    }
}