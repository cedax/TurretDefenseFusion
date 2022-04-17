using UnityEngine;
using UnityEngine.SceneManagement;

public class IniciarJuego : MonoBehaviour
{
    public void Iniciar()
    {
        SceneManager.LoadScene("Juego");
    }

}
