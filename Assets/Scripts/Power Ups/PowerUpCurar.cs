using UnityEngine;

public class PowerUpCurar : MonoBehaviour {
    public void Curar(){
        ManagerVida.Instancia.SumarVida(40);
        GameObject padre = transform.parent.gameObject;
        Destroy(padre);
    }
}