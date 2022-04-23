/*
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Economia))]
public class EconomiaEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Economia economia = (Economia)target;

        if (GUILayout.Button("Agregar Monedas")){
            economia.AgregarMonedas(100);
        }
    }
}
*/