using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    public void PerderJuego(){
        SceneManager.LoadScene("GameOver");
    }
}