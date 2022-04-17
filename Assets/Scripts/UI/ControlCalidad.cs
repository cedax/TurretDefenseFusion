using UnityEngine;
using TMPro;

public class ControlCalidad : MonoBehaviour{
    public TMP_Dropdown dropdown;
    public int calidad;

    public void Start(){
        calidad = PlayerPrefs.GetInt("numeroDeCalidad", 2);
        dropdown.value = calidad;
        CambiarCalidad();
    }

    public void CambiarCalidad(){
        calidad = dropdown.value;
        QualitySettings.SetQualityLevel(calidad);
        PlayerPrefs.SetInt("numeroDeCalidad", calidad);
    }
}