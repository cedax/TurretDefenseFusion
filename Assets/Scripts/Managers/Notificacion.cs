using UnityEngine;
using TMPro;

public class Notificacion : Singleton<Notificacion> {
    [SerializeField] private GameObject notificacionObjeto;
    [SerializeField] private TextMeshProUGUI notificacionTexto;

    protected override void Awake() {
        notificacionObjeto.SetActive(false);
    }

    public void Nueva(string texto){
        CancelInvoke("OcultarNotificacion");
        notificacionTexto.text = texto;
        notificacionObjeto.SetActive(true);
        Invoke("OcultarNotificacion", 4);
    }

    private void OcultarNotificacion(){
        notificacionObjeto.SetActive(false);
    }
}