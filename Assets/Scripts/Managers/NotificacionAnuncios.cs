using UnityEngine;
using TMPro;

public class NotificacionAnuncios : Singleton<NotificacionAnuncios>
{
    [SerializeField] private GameObject canvasNotificacion;
    [SerializeField] private GameObject canvasPrincipal;
    [SerializeField] private TextMeshProUGUI textoNotificacion;

    public void MostrarNotificacion(string mensaje)
    {
        canvasPrincipal.SetActive(false);
        canvasNotificacion.SetActive(true);
        textoNotificacion.text = mensaje;
    }
}
