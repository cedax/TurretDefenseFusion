using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
    private float deltaTime;
    private float fps;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        fps = 1.0f / deltaTime;
    }

    private void Start() {
        InvokeRepeating("AjustarCalidad", 2, 2);
    }

    private void AjustarCalidad(){
        if(fps >= 25)
        {
            int calidad = 3;
            QualitySettings.SetQualityLevel(calidad);
            PlayerPrefs.SetInt("numeroDeCalidad", calidad);
        }
        else if(fps >= 20 && fps <= 24)
        {
            int calidad = 2;
            QualitySettings.SetQualityLevel(calidad);
            PlayerPrefs.SetInt("numeroDeCalidad", calidad);
        }
        else
        {
            int calidad = 0;
            QualitySettings.SetQualityLevel(calidad);
            PlayerPrefs.SetInt("numeroDeCalidad", calidad);
        }
    }
}