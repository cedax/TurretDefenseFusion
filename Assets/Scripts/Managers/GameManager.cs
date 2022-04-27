using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    public void PerderJuego(){
        GameOver.Instancia.Perdiste();
    }
}